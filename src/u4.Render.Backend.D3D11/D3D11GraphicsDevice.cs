using System.Diagnostics.CodeAnalysis;
using System.Text;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using u4.Math;
using static TerraFX.Interop.DirectX.D3D_DRIVER_TYPE;
using static TerraFX.Interop.DirectX.D3D_FEATURE_LEVEL;
using static TerraFX.Interop.DirectX.D3D11_CREATE_DEVICE_FLAG;
using static TerraFX.Interop.DirectX.DirectX;
using static TerraFX.Interop.DirectX.D3D11;
using static TerraFX.Interop.DirectX.DXGI;
using static TerraFX.Interop.DirectX.DXGI_FORMAT;
using static TerraFX.Interop.DirectX.DXGI_SWAP_CHAIN_FLAG;
using static TerraFX.Interop.DirectX.DXGI_SWAP_EFFECT;
using static TerraFX.Interop.Windows.Windows;

namespace u4.Render.Backend.D3D11;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public sealed unsafe class D3D11GraphicsDevice : GraphicsDevice
{
    private const uint SwapchainFlags =
        (uint) (DXGI_SWAP_CHAIN_FLAG_ALLOW_TEARING | DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH);

    private Viewport _viewport;
    
    private IDXGISwapChain* _swapchain;
    private ID3D11Texture2D* _swapchainTexture;
    private ID3D11RenderTargetView* _swapchainTarget;
    
    public ID3D11Device* Device;
    public ID3D11DeviceContext* Context;

    public override GraphicsApi Api => GraphicsApi.Direct3D11;

    public override Viewport Viewport
    {
        get => _viewport;
        set
        {
            _viewport = value;
            D3D11_VIEWPORT viewport = new D3D11_VIEWPORT()
            {
                TopLeftX = value.X,
                TopLeftY = value.Y,
                Width = value.Width,
                Height = value.Height,
                MinDepth = value.MinDepth,
                MaxDepth = value.MaxDepth
            };

            Context->RSSetViewports(1, &viewport);
        }
    }

    public D3D11GraphicsDevice(nint hwnd, Size<uint> swapchainSize)
    {
        DXGI_SWAP_CHAIN_DESC swapChainDesc = new()
        {
            OutputWindow = (HWND) hwnd,
            Windowed = true,

            BufferCount = 2,
            BufferDesc = new DXGI_MODE_DESC() { Width = swapchainSize.Width, Height = swapchainSize.Height, Format = DXGI_FORMAT_B8G8R8A8_UNORM },
            BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT,

            SampleDesc = new DXGI_SAMPLE_DESC(1, 0),
            SwapEffect = DXGI_SWAP_EFFECT_FLIP_DISCARD,

            Flags = SwapchainFlags
        };

        uint flags = (uint) (D3D11_CREATE_DEVICE_BGRA_SUPPORT | D3D11_CREATE_DEVICE_DEBUG);
        D3D_FEATURE_LEVEL level = D3D_FEATURE_LEVEL_11_0;

        IDXGISwapChain* swapchain;
        ID3D11Device* device;
        ID3D11DeviceContext* context;
        if (FAILED(D3D11CreateDeviceAndSwapChain(null, D3D_DRIVER_TYPE_HARDWARE, HMODULE.NULL, flags, &level, 1,
                D3D11_SDK_VERSION, &swapChainDesc, &swapchain, &device, null, &context)))
        {
            throw new Exception("Failed to create D3D11 device.");
        }

        _swapchain = swapchain;
        Device = device;
        Context = context;

        ID3D11Texture2D* swapchainTexture;
        if (FAILED(_swapchain->GetBuffer(0, __uuidof<ID3D11Texture2D>(), (void**) &swapchainTexture)))
            throw new Exception("Failed to get swapchain buffer.");
        _swapchainTexture = swapchainTexture;

        ID3D11RenderTargetView* swapchainTarget;
        if (FAILED(Device->CreateRenderTargetView((ID3D11Resource*) swapchainTexture, null, &swapchainTarget)))
            throw new Exception("Failed to create swapchain target.");
        _swapchainTarget = swapchainTarget;

        Viewport = new Viewport(0, 0, swapchainSize.Width, swapchainSize.Height);
    }

    public override void ClearColorBuffer(Color color)
    {
        Context->ClearRenderTargetView(_swapchainTarget, &color.R);
    }

    public override GraphicsBuffer CreateBuffer<T>(in BufferDescription description, in ReadOnlySpan<T> data)
    {
        fixed (void* pData = data)
            return new D3D11GraphicsBuffer(Device, description, pData);
    }

    public override ShaderModule CreateShaderModuleFromFile(string path, ShaderStage stage, string entryPoint)
    {
        return D3D11ShaderModule.FromFile(path, stage, Encoding.UTF8.GetBytes(entryPoint));
    }

    public override Shader CreateShader(in ReadOnlySpan<ShaderAttachment> attachments)
    {
        return new D3D11Shader(Device, attachments);
    }

    public override void SetPrimitiveType(PrimitiveType type)
    {
        Context->IASetPrimitiveTopology(type.ToPrimitiveTopology());
    }

    public override void SetShader(Shader shader)
    {
        D3D11Shader d3dShader = (D3D11Shader) shader;

        for (int i = 0; i < d3dShader.Objects.Length; i++)
        {
            ref D3D11Shader.ShaderObject obj = ref d3dShader.Objects[i];

            switch (obj.Stage)
            {
                case ShaderStage.Vertex:
                    Context->VSSetShader((ID3D11VertexShader*) obj.Shader, null, 0);
                    break;
                case ShaderStage.Pixel:
                    Context->PSSetShader((ID3D11PixelShader*) obj.Shader, null, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public override void Draw(uint vertexCount)
    {
        Context->Draw(vertexCount, 0);
    }

    public override void DrawIndexed(uint indexCount)
    {
        Context->DrawIndexed(indexCount, 0, 0);
    }

    public override void Present()
    {
        _swapchain->Present(1, 0);

        ID3D11RenderTargetView* target = _swapchainTarget;
        Context->OMSetRenderTargets(1, &target, null);
    }

    public override void ResizeSwapchain(in Size<int> size)
    {
        // Unset the render targets.
        Context->OMSetRenderTargets(0, null, null);

        _swapchainTarget->Release();
        _swapchainTexture->Release();

        if (FAILED(_swapchain->ResizeBuffers(0, (uint) size.Width, (uint) size.Height, DXGI_FORMAT_UNKNOWN, SwapchainFlags)))
            throw new Exception("Failed to resize swapchain buffers.");

        ID3D11Texture2D* swapchainTexture;
        if (FAILED(_swapchain->GetBuffer(0, __uuidof<ID3D11Texture2D>(), (void**) &swapchainTexture)))
            throw new Exception("Failed to get swapchain buffer.");
        _swapchainTexture = swapchainTexture;

        ID3D11RenderTargetView* swapchainTarget;
        if (FAILED(Device->CreateRenderTargetView((ID3D11Resource*) swapchainTexture, null, &swapchainTarget)))
            throw new Exception("Failed to create swapchain target.");
        _swapchainTarget = swapchainTarget;
    }

    public override void Dispose()
    {
        _swapchainTarget->Release();
        _swapchainTexture->Release();
        _swapchain->Release();

        Context->Release();
        Device->Release();
    }
}
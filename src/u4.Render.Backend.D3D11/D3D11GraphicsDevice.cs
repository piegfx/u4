using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using u4.Math;
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
public unsafe class D3D11GraphicsDevice : GraphicsDevice
{
    private IDXGISwapChain* _swapchain;
    private ID3D11Texture2D* _swapchainTexture;
    private ID3D11RenderTargetView* _swapchainTarget;
    
    public ID3D11Device* Device;
    public ID3D11DeviceContext* Context;
    
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

            Flags = (uint) (DXGI_SWAP_CHAIN_FLAG_ALLOW_TEARING | DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH)
        };

        uint flags = (uint) (D3D11_CREATE_DEVICE_BGRA_SUPPORT | D3D11_CREATE_DEVICE_DEBUG);
        D3D_FEATURE_LEVEL level = D3D_FEATURE_LEVEL_11_0;

        IDXGISwapChain* swapchain;
        ID3D11Device* device;
        ID3D11DeviceContext* context;
        if (FAILED(D3D11CreateDeviceAndSwapChain(null, D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_HARDWARE, HMODULE.NULL, flags,
                &level, 1, D3D11_SDK_VERSION, &swapChainDesc, &swapchain, &device, null, &context)))
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
    }

    public override void Present()
    {
        _swapchain->Present(1, 0);
    }

    public override void Dispose()
    {
        _swapchainTarget->Release();
        _swapchainTarget->Release();
        _swapchain->Release();

        Context->Release();
        Device->Release();
    }
}
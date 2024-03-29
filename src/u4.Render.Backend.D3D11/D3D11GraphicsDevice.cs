﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using u4.Math;
using static TerraFX.Interop.DirectX.D3D_DRIVER_TYPE;
using static TerraFX.Interop.DirectX.D3D_FEATURE_LEVEL;
using static TerraFX.Interop.DirectX.D3D11_CREATE_DEVICE_FLAG;
using static TerraFX.Interop.DirectX.DirectX;
using static TerraFX.Interop.DirectX.D3D11;
using static TerraFX.Interop.DirectX.D3D11_MAP;
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

    public override GraphicsBuffer CreateBuffer<T>(in BufferDescription description, T data)
    {
        return new D3D11GraphicsBuffer(Device, description, Unsafe.AsPointer(ref data));
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

    public override InputLayout CreateInputLayout(in ReadOnlySpan<InputLayoutDescription> descriptions, ShaderModule vertexModule)
    {
        return new D3D11InputLayout(Device, descriptions, ((D3D11ShaderModule) vertexModule).Blob);
    }

    public override Texture CreateTexture<T>(in TextureDescription description, in ReadOnlySpan<T> data)
    {
        fixed (void* pData = data)
            return new D3D11Texture(Device, Context, description, pData);
    }

    public override void UpdateBuffer<T>(GraphicsBuffer buffer, uint offsetInBytes, uint sizeInBytes, T data)
        => UpdateBuffer(buffer, offsetInBytes, sizeInBytes, new ReadOnlySpan<T>(ref data));

    public override void UpdateBuffer<T>(GraphicsBuffer buffer, uint offsetInBytes, uint sizeInBytes, in ReadOnlySpan<T> data)
    {
        D3D11GraphicsBuffer d3dBuffer = (D3D11GraphicsBuffer) buffer;

        if (d3dBuffer.Dynamic)
        {
            D3D11_MAPPED_SUBRESOURCE subresource;
            if (FAILED(Context->Map((ID3D11Resource*) d3dBuffer.Buffer, 0, D3D11_MAP_WRITE_DISCARD, 0, &subresource)))
                throw new Exception("Failed to map buffer.");
            
            fixed (void* pData = data)
                Unsafe.CopyBlock(subresource.pData, pData, sizeInBytes);

            Context->Unmap((ID3D11Resource*) d3dBuffer.Buffer, 0);
        }
        else
        {
            D3D11_BOX box = new D3D11_BOX((int) offsetInBytes, 0, 0, (int) sizeInBytes, 1, 1);

            // TODO: Is this actually working?
            fixed (void* pData = data)
                Context->UpdateSubresource((ID3D11Resource*) d3dBuffer.Buffer, 0, &box, pData, 0, 0);
        }
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

    public override void SetTexture(uint slot, Texture texture)
    {
        D3D11Texture d3dTexture = (D3D11Texture) texture;

        ID3D11ShaderResourceView* textureSrv = d3dTexture.TextureSrv;
        Context->PSSetShaderResources(slot, 1, &textureSrv);
    }

    public override void SetInputLayout(InputLayout layout)
    {
        D3D11InputLayout d3dLayout = (D3D11InputLayout) layout;
        Context->IASetInputLayout(d3dLayout.Layout);
    }

    public override void SetVertexBuffer(uint slot, GraphicsBuffer buffer, uint stride)
    {
        D3D11GraphicsBuffer d3dBuffer = (D3D11GraphicsBuffer) buffer;
        ID3D11Buffer* vBuffer = d3dBuffer.Buffer;
        uint offset = 0;
        Context->IASetVertexBuffers(slot, 1, &vBuffer, &stride, &offset);
    }

    public override void SetIndexBuffer(GraphicsBuffer buffer, Format format)
    {
        D3D11GraphicsBuffer d3dBuffer = (D3D11GraphicsBuffer) buffer;
        Context->IASetIndexBuffer(d3dBuffer.Buffer, format.ToDxgiFormat(), 0);
    }

    public override void SetConstantBuffer(uint slot, GraphicsBuffer buffer, ShaderStage stage)
    {
        D3D11GraphicsBuffer d3dBuffer = (D3D11GraphicsBuffer) buffer;
        ID3D11Buffer* buf = d3dBuffer.Buffer;

        switch (stage)
        {
            case ShaderStage.Vertex:
                Context->VSSetConstantBuffers(slot, 1, &buf);
                break;
            case ShaderStage.Pixel:
                Context->PSSetConstantBuffers(slot, 1, &buf);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stage), stage, null);
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

    public override void GenerateMipmaps(Texture texture)
    {
        D3D11Texture d3dTexture = (D3D11Texture) texture;

        Context->GenerateMips(d3dTexture.TextureSrv);
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
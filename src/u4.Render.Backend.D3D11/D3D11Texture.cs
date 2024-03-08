using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D_SRV_DIMENSION;
using static TerraFX.Interop.DirectX.D3D11_BIND_FLAG;
using static TerraFX.Interop.DirectX.D3D11_CPU_ACCESS_FLAG;
using static TerraFX.Interop.DirectX.D3D11_RESOURCE_MISC_FLAG;
using static TerraFX.Interop.DirectX.D3D11_USAGE;
using static TerraFX.Interop.Windows.Windows;

namespace u4.Render.Backend.D3D11;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal sealed unsafe class D3D11Texture : Texture
{
    public ID3D11Resource* Texture;
    public ID3D11ShaderResourceView* TextureSrv;

    public D3D11Texture(ID3D11Device* device, ID3D11DeviceContext* context, in TextureDescription description, void* data)
    {
        D3D11_BIND_FLAG flags = 0;

        bool generateMips = false;

        if ((description.Usage & TextureUsage.ShaderResource) == TextureUsage.ShaderResource)
            flags |= D3D11_BIND_SHADER_RESOURCE;

        if ((description.Usage & TextureUsage.RenderTarget) == TextureUsage.RenderTarget)
            flags |= D3D11_BIND_RENDER_TARGET;

        if ((description.Usage & TextureUsage.GenerateMipmaps) == TextureUsage.GenerateMipmaps)
        {
            flags |= D3D11_BIND_RENDER_TARGET;
            generateMips = true;
        }

        D3D11_SHADER_RESOURCE_VIEW_DESC texSrv = new D3D11_SHADER_RESOURCE_VIEW_DESC()
        {
            Format = description.Format.ToDxgiFormat()
        };

        switch (description.Type)
        {
            case TextureType.Texture2D:
                D3D11_TEXTURE2D_DESC tex2DDesc = new D3D11_TEXTURE2D_DESC()
                {
                    Width = description.Width,
                    Height = description.Height,
                    Format = texSrv.Format,
                    MipLevels = description.MipLevels,
                    ArraySize = description.ArraySize,
                    SampleDesc = new DXGI_SAMPLE_DESC(1, 0),
                    Usage = D3D11_USAGE_DEFAULT,
                    BindFlags = (uint) flags,
                    MiscFlags = generateMips ? (uint) D3D11_RESOURCE_MISC_GENERATE_MIPS : 0,
                    CPUAccessFlags = (uint) D3D11_CPU_ACCESS_WRITE
                };

                ID3D11Texture2D* tex2D;
                if (FAILED(device->CreateTexture2D(&tex2DDesc, null, &tex2D)))
                    throw new Exception("Failed to create Texture2D.");

                Texture = (ID3D11Resource*) tex2D;

                texSrv.ViewDimension = D3D11_SRV_DIMENSION_TEXTURE2D;
                texSrv.Texture2D = new D3D11_TEX2D_SRV()
                {
                    MipLevels = uint.MaxValue,
                    MostDetailedMip = 0
                };
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if ((description.Usage & TextureUsage.ShaderResource) == TextureUsage.ShaderResource)
        {
            ID3D11ShaderResourceView* textureSrv;
            if (FAILED(device->CreateShaderResourceView(Texture, &texSrv, &textureSrv)))
                throw new Exception("Failed to create shader resource view.");

            TextureSrv = textureSrv;
        }

        if (data != null)
        {
            //context->UpdateSubresource(Texture, 0, null, data, )
        }
    }
    
    public override void Dispose()
    {
        if (TextureSrv != null)
            TextureSrv->Release();
        
        Texture->Release();
    }
}
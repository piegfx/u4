#include "D3D11Texture.h"

#include <stdexcept>

namespace u4::Graphics::D3D11 {
    D3D11Texture::D3D11Texture(ID3D11Device* device, ID3D11DeviceContext* context, const Math::Size& size, void* data) {
        _size = size;
        
        D3D11_TEXTURE2D_DESC texDesc {
            .Width = static_cast<UINT>(size.Width),
            .Height = static_cast<UINT>(size.Height),
            .MipLevels = 0,
            .ArraySize = 1,
            .Format = DXGI_FORMAT_R8G8B8A8_UNORM, // TODO: Small format enum that supports different types, such as DXT.
            .SampleDesc = { .Count = 1, .Quality = 0 },
            .Usage = D3D11_USAGE_DEFAULT,
            .BindFlags = D3D11_BIND_SHADER_RESOURCE | D3D11_BIND_RENDER_TARGET,
            .CPUAccessFlags = 0,
            .MiscFlags = D3D11_RESOURCE_MISC_GENERATE_MIPS // TODO: Support textures with pre-baked mips, or just not generating mips at all if not desired.
        };

        D3D11_SHADER_RESOURCE_VIEW_DESC srvDesc {
            .Format = DXGI_FORMAT_R8G8B8A8_UNORM,
            .ViewDimension = D3D11_SRV_DIMENSION_TEXTURE2D,
            .Texture2D = {
                .MostDetailedMip = 0,
                .MipLevels = UINT_MAX,
            }
        };

        if (FAILED(device->CreateTexture2D(&texDesc, nullptr, &Texture))) {
            throw std::runtime_error("D3D11Texture: Failed to create texture.");
        }

        if (FAILED(device->CreateShaderResourceView(Texture, &srvDesc, &ShaderResource))) {
            throw std::runtime_error("D3D11Texture: Failed to create shader resource.");
        }

        context->UpdateSubresource(Texture, 0, nullptr, data, static_cast<UINT>(size.Width * 4), 0);
        context->GenerateMips(ShaderResource);
    }

    D3D11Texture::~D3D11Texture() {
        ShaderResource->Release();
        Texture->Release();
    }

    Math::Size D3D11Texture::Size() const {
        return _size;
    }
}

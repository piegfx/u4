#pragma once

#include "u4/Graphics/Texture.h"

#include <d3d11.h>

namespace u4::Graphics::D3D11 {

    class D3D11Texture : public Texture {
    private:
        Math::Size _size;
        
    public:
        ID3D11Texture2D* Texture;
        ID3D11ShaderResourceView* ShaderResource;

        D3D11Texture(ID3D11Device* device, ID3D11DeviceContext* context, const Math::Size& size, void* data);
        ~D3D11Texture() override;

        Math::Size Size() const override;
    };
    
}

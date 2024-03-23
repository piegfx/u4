#pragma once

#include "u4/Graphics/Graphics.h"

#include <d3d11.h>

namespace u4::Graphics::D3D11 {

    class D3D11Graphics final : public Graphics {
    private:
        IDXGISwapChain* _swapChain;
        ID3D11Texture2D* _swapChainTexture;
        ID3D11RenderTargetView* _swapChainTarget;
        
    public:
        ID3D11Device* Device;
        ID3D11DeviceContext* Context;

        D3D11Graphics(SDL_Window* window, const Math::Size& size);
        ~D3D11Graphics() override;

        void Present() override;
    };
    
}

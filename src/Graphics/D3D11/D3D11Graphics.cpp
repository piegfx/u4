#include "D3D11Graphics.h"
#include "D3D11Texture.h"

#include <SDL_syswm.h>
#include <dxgi.h>
#include <stdexcept>

namespace u4::Graphics::D3D11 {
    D3D11Graphics::D3D11Graphics(SDL_Window* window, const Math::Size& size) {
        SDL_SysWMinfo info;
        SDL_VERSION(&info.version);

        SDL_GetWindowWMInfo(window, &info);

        DXGI_SWAP_CHAIN_DESC swapChainDesc {
            .BufferDesc = {
                .Width = static_cast<UINT>(size.Width),
                .Height = static_cast<UINT>(size.Height),
                .Format = DXGI_FORMAT_B8G8R8A8_UNORM
            },
            .SampleDesc = {
                .Count = 1,
                .Quality = 0
            },
            .BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT,
            .BufferCount = 2,
            .OutputWindow = info.info.win.window,
            .Windowed = TRUE,
            .SwapEffect = DXGI_SWAP_EFFECT_FLIP_DISCARD,
            .Flags = DXGI_SWAP_CHAIN_FLAG_ALLOW_TEARING | DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH
        };

        auto flags = D3D11_CREATE_DEVICE_DEBUG | D3D11_CREATE_DEVICE_BGRA_SUPPORT;
        auto featureLevel = D3D_FEATURE_LEVEL_11_0;
        
        if (FAILED(
            D3D11CreateDeviceAndSwapChain(nullptr, D3D_DRIVER_TYPE_HARDWARE, nullptr, flags, &featureLevel, 1,
                D3D11_SDK_VERSION, &swapChainDesc, &_swapChain, &Device, nullptr, &Context))) {
            throw std::runtime_error("D3D11Graphics: Failed to create D3D11 device.");
        }

        if (FAILED(_swapChain->GetBuffer(0, __uuidof(ID3D11Texture2D), (void**) &_swapChainTexture))) {
            throw std::runtime_error("D3D11Graphics: Failed to get swapchain buffer.");
        }

        if (FAILED(Device->CreateRenderTargetView(_swapChainTexture, nullptr, &_swapChainTarget))) {
            throw std::runtime_error("D3D11Graphics: Failed to create swapchain target.");
        }
    }

    D3D11Graphics::~D3D11Graphics() {
        _swapChainTarget->Release();
        _swapChainTexture->Release();
        _swapChain->Release();
        Context->Release();
        Device->Release();
    }

    std::unique_ptr<Texture> D3D11Graphics::CreateTexture(const Bitmap& bitmap) {
        return std::make_unique<D3D11Texture>(Device, Context, bitmap.Size(), (void*) bitmap.Data().data());
    }

    void D3D11Graphics::Present() {
        Context->OMSetRenderTargets(1, &_swapChainTarget, nullptr);

        float color[] = { 1.0f, 0.5f, 0.25f, 1.0f };
        Context->ClearRenderTargetView(_swapChainTarget, color);
        
        if (FAILED(_swapChain->Present(1, 0))) {
            throw std::runtime_error("Failed to present.");
        }
    }
}

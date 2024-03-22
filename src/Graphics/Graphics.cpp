#include "u4/Graphics/Graphics.h"

#include "D3D11/D3D11Graphics.h"

namespace u4::Graphics {
    std::unique_ptr<Graphics> Graphics::CreateD3D11(SDL_Window* window, const Math::Size& size) {
        return std::make_unique<D3D11::D3D11Graphics>(window, size);
    }
}

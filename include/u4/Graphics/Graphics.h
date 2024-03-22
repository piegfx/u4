#pragma once

#include "u4/Math/Size.h"

#include <SDL.h>

#include <memory>

namespace u4::Graphics {

    class Graphics {
    public:
        virtual ~Graphics() = default;

        static std::unique_ptr<Graphics> CreateD3D11(SDL_Window* window, const Math::Size& size);
    };
    
}

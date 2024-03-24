#pragma once

#include "u4/Math/Size.h"
#include "Texture.h"
#include "Bitmap.h"

#include <SDL.h>

#include <memory>

namespace u4::Graphics {

    class Graphics {
    public:
        virtual ~Graphics() = default;

        virtual std::unique_ptr<Texture> CreateTexture(const Bitmap& bitmap) = 0;

        virtual void Present() = 0;

        static std::unique_ptr<Graphics> CreateD3D11(SDL_Window* window, const Math::Size& size);
    };
    
}

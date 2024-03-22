#pragma once

#include "Math/Size.h"

#include <string>

#include <SDL.h>

namespace u4 {

    struct WindowOptions {
        Math::Size Size;
        std::string Title;
    };
    
    class Window {
    private:
        SDL_Window* _handle;

    public:
        explicit Window(const WindowOptions& options);
        
        [[nodiscard]] SDL_Window* Handle() const;

        [[nodiscard]] Math::Size Size() const;
        void SetSize(const Math::Size& size);

        [[nodiscard]] Math::Size FramebufferSize() const;
    };
    
}

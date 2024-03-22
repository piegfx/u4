#include "u4/Window.h"

#include <stdexcept>

namespace u4 {
    Window::Window(const WindowOptions& options) {
        if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_EVENTS) < 0) {
            throw std::runtime_error("Failed to initialize SDL: " + std::string(SDL_GetError()));
        }

        _handle = SDL_CreateWindow(options.Title.c_str(), SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED,
                                   options.Size.Width, options.Size.Height, SDL_WINDOW_SHOWN);

        if (!_handle) {
            throw std::runtime_error("Failed to create SDL window: " + std::string(SDL_GetError()));
        }
    }

    SDL_Window* Window::Handle() const {
        return _handle;
    }

    Math::Size Window::Size() const {
        int32 w, h;
        SDL_GetWindowSize(_handle, &w, &h);

        return { w, h };
    }

    void Window::SetSize(const Math::Size& size) {
        SDL_SetWindowSize(_handle, size.Width, size.Height);
    }

    Math::Size Window::FramebufferSize() const {
        int32 w, h;
        SDL_GetWindowSizeInPixels(_handle, &w, &h);

        return { w, h };
    }
}

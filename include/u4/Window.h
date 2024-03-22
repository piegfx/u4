#pragma once

#include <SDL.h>

namespace u4 {
    
    class Window {
    private:
        SDL_Window* _handle;

    public:
        SDL_Window* Handle();

        
    };
    
}

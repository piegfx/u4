#include <memory>

#include "u4/Window.h"

int main(int argc, char* argv[]) {
    u4::WindowOptions options {
        .Size = { 1280, 720 },
        .Title = "Test"
    };
    
    auto window = std::make_unique<u4::Window>(options);

    bool exists = true;
    while (exists) {
        SDL_Event event;
        while (SDL_PollEvent(&event)) {
            switch (event.type) {
                case SDL_WINDOWEVENT: {
                    switch (event.window.event) {
                        case SDL_WINDOWEVENT_CLOSE: {
                            exists = false;
                            break;
                        }
                    }
                    
                    break;
                }
            }
        }
    }
    
    return 0;
}
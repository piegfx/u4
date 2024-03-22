#include "u4/Window.h"
#include "u4/Math/Vector2.h"
#include "u4/Math/Vector3.h"

#include <memory>
#include <iostream>

using namespace u4::Math;

int main(int argc, char* argv[]) {
    Vector3f vecA = Vector3f(1);
    Vector3f vecB = { 0.9f, -2.3f, 4.5f };

    Vector3f vecC = vecA + vecB;
    std::cout << vecC.ToString() << std::endl;
    std::cout << "Length: " << vecC.Length() << ", Normalized: " << vecC.Normalize().ToString() << std::endl;
    
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
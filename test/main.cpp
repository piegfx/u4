#include "u4/Window.h"
#include "u4/Graphics/Graphics.h"
#include "u4/Math/Vector2.h"
#include "u4/Math/Vector3.h"
#include "u4/Math/Vector4.h"
#include "u4/Math/Matrix4.h"

#include <memory>
#include <iostream>

using namespace u4::Math;
using namespace u4::Graphics;

int main(int argc, char* argv[]) {
    Vector4f vecA = Vector4f(1);
    Vector4f vecB = { 0.9f, -2.3f, 4.5f, 2 };

    Vector4f vecC = vecA + vecB;
    std::cout << vecC.ToString() << std::endl;
    std::cout << "Length: " << vecC.Length() << ", Normalized: " << vecC.Normalize().ToString() << std::endl;

    Matrix4f matA = {
        1, 2, 3, 4,
        5, 6, 7, 8,
        9, 10, 11, 12,
        13, 14, 15, 16
    };

    Matrix4f matB = {
        16, 15, 14, 13,
        12, 11, 10, 9,
        8, 7, 6, 5,
        4, 3, 2, 1
    };

    Matrix4f matC = matA * matB;
    std::cout << matC.ToString() << std::endl;
    
    u4::WindowOptions options {
        .Size = { 1280, 720 },
        .Title = "Test"
    };
    
    auto window = std::make_unique<u4::Window>(options);
    auto graphics = Graphics::CreateD3D11(window->Handle(), window->FramebufferSize());

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
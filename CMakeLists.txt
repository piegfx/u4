cmake_minimum_required(VERSION 3.28)
project(u4)

set(CMAKE_CXX_STANDARD 17)

add_library(u4 STATIC library.cpp
        src/Window.cpp
        include/u4/Window.h)

target_include_directories(${PROJECT_NAME} PRIVATE include/)

# ---------- SDL2 ----------
add_subdirectory(lib/SDL)

if (TARGET SDL2::SDL2main)
    target_link_libraries(${PROJECT_NAME} SDL2::SDL2main)
endif ()

target_link_libraries(${PROJECT_NAME} SDL2::SDL2-static)

# ---------- D3D11 ----------
target_link_libraries(${PROJECT_NAME} d3d11 dxgi d3dcompiler)
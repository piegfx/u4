cmake_minimum_required(VERSION 3.28)
project(u4)

set(CMAKE_CXX_STANDARD 17)

add_library(u4 STATIC
        src/Window.cpp
        include/u4/Window.h
        include/u4/Math/Size.h
        include/u4/Math/Types.h
        include/u4/Math/Vector2.h
        include/u4/Math/Vector3.h
        include/u4/Math/Vector4.h)

target_include_directories(${PROJECT_NAME} PRIVATE include/)

# ---------- SDL2 ----------
add_subdirectory(lib/SDL)

if (TARGET SDL2::SDL2main)
    target_link_libraries(${PROJECT_NAME} PUBLIC SDL2::SDL2main)
endif ()

target_link_libraries(${PROJECT_NAME} PUBLIC SDL2::SDL2-static)

# ---------- D3D11 ----------
target_link_libraries(${PROJECT_NAME} PUBLIC d3d11 dxgi d3dcompiler)

# ----- TEST -----
add_executable(u4test test/main.cpp)

target_link_libraries(u4test PUBLIC u4)
target_include_directories(u4test PUBLIC include)
#include "u4/Graphics/Bitmap.h"

#define STB_IMAGE_IMPLEMENTATION
#include <stb_image.h>

#include <stdexcept>

namespace u4::Graphics {
    Bitmap::Bitmap(const std::string& path) {
        int w, h;
        uint8* data = stbi_load(path.c_str(), &w, &h, nullptr, 4);

        if (!data) {
            throw std::runtime_error("Failed to load image: " + std::string(stbi_failure_reason()));
        }

        _data = std::vector(data, data + (w * h * 4));
        _size = { w, h };
    }

    const std::vector<uint8>& Bitmap::Data() const {
        return _data;
    }

    Math::Size Bitmap::Size() const {
        return _size;
    }
}

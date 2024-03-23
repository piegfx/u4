#pragma once

#include "../Math/Types.h"
#include "../Math/Size.h"

#include <vector>
#include <string>

namespace u4::Graphics {

    class Bitmap {
    private:
        std::vector<uint8> _data;
        Math::Size _size;

    public:
        Bitmap(const std::string& path);
        
        const std::vector<uint8>& Data() const;
        Math::Size Size() const;
    };
    
}

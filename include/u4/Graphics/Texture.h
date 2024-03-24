#pragma once

#include "u4/Math/Size.h"

namespace u4::Graphics {

    class Texture {
    public:
        virtual ~Texture() = default;
        
        virtual Math::Size Size() const = 0;
    };
    
}

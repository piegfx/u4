#pragma once

#include "Types.h"

namespace u4::Math {

    struct Size {
        int32 Width;
        int32 Height;

        Size(const int32 width, const int32 height) {
            Width = width;
            Height = height;
        }

        explicit Size(const int32 wh) {
            Width = wh;
            Height = wh;
        }

        Size() {
            Width = 0;
            Height = 0;
        }
    };
    
}

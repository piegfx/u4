#pragma once

#include "Types.h"

#include "string"

namespace u4::Math {

    template<typename T>
    struct Vector2 {
        T X;
        T Y;

        Vector2(T x, T y) {
            X = x;
            Y = y;
        }

        explicit Vector2(T scalar) {
            X = scalar;
            Y = scalar;
        }

        Vector2() {
            X = 0;
            Y = 0;
        }

        template<typename TOther>
        Vector2 As() {
            return { static_cast<TOther>(X), static_cast<TOther>(X) };
        }

        T Length() const {
            return sqrt(Dot(*this, *this));
        }

        Vector2 Normalize() const {
            return *this / Length();
        }

        std::string ToString() const {
            return "{ X: " + std::to_string(X) + ", Y: " + std::to_string(Y) + " }";
        }

        Vector2 operator +(const Vector2& other) const {
            return { X + other.X, Y + other.Y };
        }

        Vector2 operator -(const Vector2& other) const {
            return { X - other.X, Y - other.Y };
        }

        Vector2 operator *(const Vector2& other) const {
            return { X * other.X, Y * other.Y };
        }

        Vector2 operator *(T other) const {
            return { X * other, Y * other };
        }

        Vector2 operator /(const Vector2& other) const {
            return { X / other.X, Y / other.Y };
        }

        Vector2 operator /(T other) const {
            return { X / other, Y / other };
        }

        static T Dot(const Vector2& a, const Vector2& b) {
            return a.X * b.X + a.Y * b.Y;
        }
    };

    typedef Vector2<float> Vector2f;
    typedef Vector2<double> Vector2d;
    typedef Vector2<int32> Vector2i;

}
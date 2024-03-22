#pragma once

#include "Types.h"

#include <string>

namespace u4::Math {

    template<typename T>
    struct Vector4 {
        T X;
        T Y;
        T Z;
        T W;

        Vector4(T x, T y, T z, T w) {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        explicit Vector4(T scalar) {
            X = scalar;
            Y = scalar;
            Z = scalar;
            W = scalar;
        }

        Vector4() {
            X = 0;
            Y = 0;
            Z = 0;
            W = 0;
        }

        template<typename TOther>
        Vector4 As() {
            return { static_cast<TOther>(X), static_cast<TOther>(Y), static_cast<TOther>(Z), static_cast<TOther>(W) };
        }

        T Length() const {
            return sqrt(Dot(*this, *this));
        }

        Vector4 Normalize() const {
            return *this / Length();
        }

        std::string ToString() const {
            return "{ X: " + std::to_string(X) + ", Y: " + std::to_string(Y) + ", Z: " + std::to_string(Z) + ", W: " + std::to_string(W) + " }";
        }

        Vector4 operator +(const Vector4& other) const {
            return { X + other.X, Y + other.Y, Z + other.Z, W + other.W };
        }

        Vector4 operator -(const Vector4& other) const {
            return { X - other.X, Y - other.Y, Z - other.Z, W - other.W };
        }

        Vector4 operator *(const Vector4& other) const {
            return { X * other.X, Y * other.Y, Z * other.Z, W * other.W };
        }

        Vector4 operator *(T other) const {
            return { X * other, Y * other, Z * other, W * other };
        }

        Vector4 operator /(const Vector4& other) const {
            return { X / other.X, Y / other.Y, Z / other.Z, W / other.W };
        }

        Vector4 operator /(T other) const {
            return { X / other, Y / other, Z / other, W / other };
        }

        static T Dot(const Vector4& a, const Vector4& b) {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }
    };

    typedef Vector4<float> Vector4f;
    typedef Vector4<double> Vector4d;
    typedef Vector4<int32> Vector4i;

}
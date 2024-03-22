#pragma once

#include "Types.h"

#include <string>

namespace u4::Math {

    template<typename T>
    struct Vector3 {
        T X;
        T Y;
        T Z;

        Vector3(T x, T y, T z) {
            X = x;
            Y = y;
            Z = z;
        }

        explicit Vector3(T scalar) {
            X = scalar;
            Y = scalar;
            Z = scalar;
        }

        Vector3() {
            X = 0;
            Y = 0;
            Z = 0;
        }

        template<typename TOther>
        Vector3 As() {
            return { static_cast<TOther>(X), static_cast<TOther>(Y), static_cast<TOther>(Z) };
        }

        T Length() const {
            return sqrt(Dot(*this, *this));
        }

        Vector3 Normalize() const {
            return *this / Length();
        }

        std::string ToString() const {
            return "{ X: " + std::to_string(X) + ", Y: " + std::to_string(Y) + ", Z: " + std::to_string(Z) + " }";
        }

        Vector3 operator +(const Vector3& other) const {
            return { X + other.X, Y + other.Y, Z + other.Z };
        }

        Vector3 operator -(const Vector3& other) const {
            return { X - other.X, Y - other.Y, Z - other.Z };
        }

        Vector3 operator *(const Vector3& other) const {
            return { X * other.X, Y * other.Y, Z * other.Z };
        }

        Vector3 operator *(T other) const {
            return { X * other, Y * other, Z * other };
        }

        Vector3 operator /(const Vector3& other) const {
            return { X / other.X, Y / other.Y, Z / other.Z };
        }

        Vector3 operator /(T other) const {
            return { X / other, Y / other, Z / other };
        }

        static T Dot(const Vector3& a, const Vector3& b) {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
    };

    typedef Vector3<float> Vector3f;
    typedef Vector3<double> Vector3d;
    typedef Vector3<int32> Vector3i;

}
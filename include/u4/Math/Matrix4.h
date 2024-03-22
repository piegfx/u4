#pragma once

#include "Types.h"
#include "Vector4.h"

#include <string>

namespace u4::Math {

    template<typename T>
    struct Matrix4 {
        Vector4<T> Row0;
        Vector4<T> Row1;
        Vector4<T> Row2;
        Vector4<T> Row3;

        Matrix4(const Vector4<T>& row0, const Vector4<T>& row1, const Vector4<T>& row2, const Vector4<T>& row3) {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        Matrix4(T m00, T m01, T m02, T m03, T m10, T m11, T m12, T m13, T m20, T m21, T m22, T m23, T m30, T m31, T m32, T m33) {
            Row0 = { m00, m01, m02, m03 };
            Row1 = { m10, m11, m12, m13 };
            Row2 = { m20, m21, m22, m23 };
            Row3 = { m30, m31, m32, m33 };
        }

        Vector4<T> Column0() const {
            return { Row0.X, Row1.X, Row2.X, Row3.X };
        }

        Vector4<T> Column1() const {
            return { Row0.Y, Row1.Y, Row2.Y, Row3.Y };
        }

        Vector4<T> Column2() const {
            return { Row0.Z, Row1.Z, Row2.Z, Row3.Z };
        }

        Vector4<T> Column3() const {
            return { Row0.W, Row1.W, Row2.W, Row3.W };
        }

        std::string ToString() const {
            return "{ Row0: " + Row0.ToString() + ", Row1: " + Row1.ToString() + ", Row2: " + Row2.ToString() + ", Row3: " + Row3.ToString() + " }";
        }

        Matrix4 operator *(const Matrix4& other) const {
            auto col0 = other.Column0();
            auto col1 = other.Column1();
            auto col2 = other.Column2();
            auto col3 = other.Column3();

            Vector4<T> row0 = {
                Vector4<T>::Dot(Row0, col0),
                Vector4<T>::Dot(Row0, col1),
                Vector4<T>::Dot(Row0, col2),
                Vector4<T>::Dot(Row0, col3),
            };

            Vector4<T> row1 = {
                Vector4<T>::Dot(Row1, col0),
                Vector4<T>::Dot(Row1, col1),
                Vector4<T>::Dot(Row1, col2),
                Vector4<T>::Dot(Row1, col3),
            };

            Vector4<T> row2 = {
                Vector4<T>::Dot(Row2, col0),
                Vector4<T>::Dot(Row2, col1),
                Vector4<T>::Dot(Row2, col2),
                Vector4<T>::Dot(Row2, col3),
            };

            Vector4<T> row3 = {
                Vector4<T>::Dot(Row3, col0),
                Vector4<T>::Dot(Row3, col1),
                Vector4<T>::Dot(Row3, col2),
                Vector4<T>::Dot(Row3, col3),
            };

            return { row0, row1, row2, row3 };
        }
    };

    typedef Matrix4<float> Matrix4f;
    typedef Matrix4<double> Matrix4d;
    typedef Matrix4<int32> Matrix4i;
    
}

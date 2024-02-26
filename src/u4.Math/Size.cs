using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace u4.Math;

[StructLayout(LayoutKind.Sequential)]
public struct Size<T> : IEquatable<Size<T>> where T : INumber<T>
{
    public T Width;

    public T Height;

    public Size(T wh)
    {
        Width = wh;
        Height = wh;
    }

    public Size(T width, T height)
    {
        Width = width;
        Height = height;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Size<TOther> As<TOther>() where TOther : INumber<TOther>
        => new Size<TOther>(TOther.CreateChecked(Width), TOther.CreateChecked(Height));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator System.Drawing.Size(in Size<T> size)
        => new System.Drawing.Size(int.CreateChecked(size.Width), int.CreateChecked(size.Height));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Vector2(in Size<T> size)
        => new Vector2(float.CreateChecked(size.Width), float.CreateChecked(size.Height));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Size<T>(in System.Drawing.Size size)
        => new Size<T>(T.CreateChecked(size.Width), T.CreateChecked(size.Height));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Size<T> operator +(in Size<T> left, in Size<T> right)
        => new Size<T>(left.Width + right.Width, left.Height + right.Height);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Size<T> operator -(in Size<T> left, in Size<T> right)
        => new Size<T>(left.Width - right.Width, left.Height - right.Height);
    
    public static bool operator ==(Size<T> left, Size<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Size<T> left, Size<T> right)
    {
        return !left.Equals(right);
    }
    
    public bool Equals(Size<T> other)
    {
        return Width == other.Width && Height == other.Height;
    }

    public override bool Equals(object obj)
    {
        return obj is Size<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Width, Height);
    }

    public override string ToString()
    {
        return $"(Width: {Width}, Height: {Height})";
    }
}

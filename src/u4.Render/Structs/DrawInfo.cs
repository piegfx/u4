using System.Numerics;

namespace u4.Render.Structs;

public struct DrawInfo
{
    public Matrix4x4 World;

    public DrawInfo(Matrix4x4 world)
    {
        World = world;
    }
}
using System.Numerics;

namespace u4.Engine.Entities;

public struct Transform
{
    public Vector3 Position;

    public Quaternion Orientation;

    public Vector3 Scale;

    public Transform()
    {
        Position = Vector3.Zero;
        Orientation = Quaternion.Identity;
        Scale = Vector3.One;
    }

    public Transform(Vector3 position) : this()
    {
        Position = position;
    }
}
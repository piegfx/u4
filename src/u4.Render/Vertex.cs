using System.Numerics;
using u4.Math;

namespace u4.Render;

public struct Vertex
{
    public readonly Vector3 Position;
    public readonly Vector2 TexCoord;
    public readonly Vector3 Normal;
    public readonly Color Color;

    public Vertex(Vector3 position, Vector2 texCoord, Vector3 normal, Color color)
    {
        Position = position;
        TexCoord = texCoord;
        Normal = normal;
        Color = color;
    }

    public Vertex(Vector3 position, Vector2 texCoord, Vector3 normal) :
        this(position, texCoord, normal, Color.White) { }

    public const uint SizeInBytes = 48;
}
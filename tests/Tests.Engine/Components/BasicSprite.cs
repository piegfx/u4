using System.Numerics;
using u4.Engine.Entities;
using u4.Math;
using u4.Render;

namespace Tests.Engine.Components;

public class BasicSprite : Component
{
    public Texture Texture;

    public float Rotation;

    public BasicSprite(Texture texture)
    {
        Texture = texture;
        Rotation = 0;
    }

    public override void Draw()
    {
        Vector2 position = new Vector2(Transform.Position.X, Transform.Position.Y);
        
        Graphics.SpriteRenderer.Draw(Texture, position, Color.White, Rotation, Vector2.One, Vector2.Zero);
    }
}
using System.Numerics;
using u4.Engine.Entities;
using u4.Render;

namespace Tests.Engine.Components;

public class BasicSprite : Component
{
    public Texture Texture;

    public BasicSprite(Texture texture)
    {
        Texture = texture;
    }

    public override void Draw()
    {
        Vector2 position = new Vector2(Transform.Position.X, Transform.Position.Y);
        
        Graphics.SpriteRenderer.Draw(Texture, position);
    }
}
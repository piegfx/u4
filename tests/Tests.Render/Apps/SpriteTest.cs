using System.Numerics;
using u4.Math;
using u4.Render;
using u4.Render.Renderers;

namespace Tests.Render.Apps;

public class SpriteTest : TestApp
{
    private Texture _texture;
    private float _value;

    protected override void Initialize()
    {
        base.Initialize();

        _texture = new Texture(@"C:\Users\ollie\Pictures\BAGELMIP.png");
    }

    protected override void Update(float dt)
    {
        base.Update(dt);

        _value += 1 * dt;
    }

    protected override void Draw()
    {
        base.Draw();

        Color clearColor = Color.CornflowerBlue;
        Graphics.Device.ClearColorBuffer(clearColor);

        ref SpriteRenderer renderer = ref Graphics.SpriteRenderer;
        
        renderer.Begin();

        for (int i = 0; i < 10; i++)
        {
            renderer.Draw(_texture, new Vector2(i * 50));
        }
        
        renderer.End();
    }
}
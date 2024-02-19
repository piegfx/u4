using System.Numerics;
using u4.Math;
using u4.Render;
using u4.Render.Renderers;

namespace Tests.Render.Apps;

public class SpriteTest : TestApp
{
    private Texture _texture;

    protected override void Initialize()
    {
        base.Initialize();

        _texture = new Texture(@"C:\Users\ollie\Pictures\awesomeface.png");
    }

    protected override void Draw()
    {
        base.Draw();

        Color clearColor = Color.CornflowerBlue;
        Graphics.Device.ClearColorBuffer(clearColor);

        ref SpriteRenderer renderer = ref Graphics.SpriteRenderer;
        
        renderer.Begin();
        renderer.Draw(_texture, new Vector2(0, 0), new Vector2(1280, 0), new Vector2(0, 512), new Vector2(512, 512),
            Color.Red);
        renderer.End();
    }
}
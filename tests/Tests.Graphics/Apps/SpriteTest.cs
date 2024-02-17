using u4.Math;
using u4.Render;

namespace Tests.Graphics.Apps;

public class SpriteTest : TestApp
{
    protected override void Draw()
    {
        base.Draw();

        Color clearColor = Color.CornflowerBlue;
        GraphicsDevice.ClearColorBuffer(clearColor);
    }
}
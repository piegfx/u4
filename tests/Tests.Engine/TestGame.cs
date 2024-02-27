using u4.Engine;
using u4.Math;
using u4.Render;

namespace Tests.Engine;

public class TestGame : Game
{
    public override void Draw()
    {
        Graphics.Device.ClearColorBuffer(Color.CornflowerBlue);
        
        base.Draw();
    }
}
using System.Numerics;
using u4.Math;

namespace u4.Render.Renderers;

public class DeferredRenderer : Renderer
{
    public override Color ClearColor { get; set; }

    public override void Draw(Renderable renderable, Matrix4x4 world)
    {
        throw new System.NotImplementedException();
    }

    public override void Render3D()
    {
        throw new System.NotImplementedException();
    }

    public override void Dispose()
    {
        throw new System.NotImplementedException();
    }
}
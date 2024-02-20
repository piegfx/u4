using System.Numerics;

namespace u4.Render.Structs;

public struct TransformedRenderable
{
    public Renderable Renderable;
    
    public Matrix4x4 World;

    public TransformedRenderable(Renderable renderable, Matrix4x4 world)
    {
        Renderable = renderable;
        World = world;
    }
}
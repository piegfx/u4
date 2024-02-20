using System;
using System.Numerics;
using u4.Math;

namespace u4.Render.Renderers;

public abstract class Renderer : IDisposable
{
    public abstract Color ClearColor { get; set; }
    
    public abstract void Draw(Renderable renderable, Matrix4x4 world);

    public abstract void Render3D();
    
    public abstract void Dispose();
}
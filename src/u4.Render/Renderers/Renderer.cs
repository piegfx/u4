using System;
using System.Numerics;
using u4.Math;
using u4.Render.Structs;

namespace u4.Render.Renderers;

public abstract class Renderer : IDisposable
{
    public Size<int> Size { get; protected set; }
    
    public virtual Color ClearColor { get; set; }

    protected Renderer(Size<int> size)
    {
        Size = size;
    }

    public abstract void Clear();
    
    public abstract void Draw(Renderable renderable, Matrix4x4 world);

    public abstract void Perform3DPass(in CameraInfo camera);

    public abstract void Render();
    
    public abstract void Dispose();
}
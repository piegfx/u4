namespace u4.Render.Backend;

public abstract class GraphicsBuffer : IDisposable
{
    public abstract BufferType Type { get; }
    
    public abstract void Dispose();
}
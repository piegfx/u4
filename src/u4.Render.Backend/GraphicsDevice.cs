namespace u4.Render.Backend;

public abstract class GraphicsDevice : IDisposable
{
    public abstract void Present();

    public abstract void Dispose();
}
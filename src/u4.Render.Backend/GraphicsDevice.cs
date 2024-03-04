namespace u4.Render.Backend;

public abstract class GraphicsDevice : IDisposable
{
    public abstract GraphicsApi Api { get; }

    public abstract GraphicsBuffer CreateBuffer<T>(in BufferDescription description, in ReadOnlySpan<T> data)
        where T : unmanaged;

    public abstract ShaderModule CreateShaderModuleFromFile(string path, ShaderStage stage, string entryPoint);
    
    public abstract void Present();

    public abstract void Dispose();
}
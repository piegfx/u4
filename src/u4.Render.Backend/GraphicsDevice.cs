﻿using u4.Math;

namespace u4.Render.Backend;

public abstract class GraphicsDevice : IDisposable
{
    public abstract GraphicsApi Api { get; }
    
    public abstract Viewport Viewport { get; set; }

    public abstract void ClearColorBuffer(Color color);

    public abstract GraphicsBuffer CreateBuffer<T>(in BufferDescription description, in ReadOnlySpan<T> data)
        where T : unmanaged;

    public abstract ShaderModule CreateShaderModuleFromFile(string path, ShaderStage stage, string entryPoint);

    public abstract Shader CreateShader(in ReadOnlySpan<ShaderAttachment> attachments);

    public abstract void SetPrimitiveType(PrimitiveType type);
    
    public abstract void SetShader(Shader shader);

    public abstract void Draw(uint vertexCount);
    
    public abstract void DrawIndexed(uint indexCount);
    
    public abstract void Present();

    public abstract void ResizeSwapchain(in Size<int> size);

    public abstract void Dispose();
}
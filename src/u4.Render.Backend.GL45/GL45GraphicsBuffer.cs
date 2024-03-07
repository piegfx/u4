using Silk.NET.OpenGL;

namespace u4.Render.Backend.GL45;

internal sealed class GL45GraphicsBuffer : GraphicsBuffer
{
    private GL _gl;
    
    public uint Buffer;
    
    public override BufferType Type { get; }

    public unsafe GL45GraphicsBuffer(GL gl, in BufferDescription description, void* data)
    {
        _gl = gl;
        Type = description.Type;
        
        Buffer = gl.CreateBuffer();
        gl.NamedBufferData(Buffer, description.SizeInBytes, data,
            description.Dynamic ? VertexBufferObjectUsage.DynamicDraw : VertexBufferObjectUsage.StaticDraw);
    }
    
    public override void Dispose()
    {
        _gl.DeleteBuffer(Buffer);
    }
}
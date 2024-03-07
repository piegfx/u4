using Silk.NET.OpenGL;
using u4.Math;

namespace u4.Render.Backend.GL45;

public class GL45GraphicsDevice : GraphicsDevice
{
    private GLContext _context;
    private GL _gl;
    
    public override GraphicsApi Api => GraphicsApi.OpenGL45;
    
    public override Viewport Viewport { get; set; }

    public GL45GraphicsDevice(GLContext context, in Size<int> size)
    {
        _context = context;

        _gl = GL.GetApi(_context.GetProcAddressFunc);
    }
    
    public override void ClearColorBuffer(Color color)
    {
        _gl.ClearColor(color.R, color.G, color.B, color.A);
        _gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    public override GraphicsBuffer CreateBuffer<T>(in BufferDescription description, in ReadOnlySpan<T> data)
    {
        throw new NotImplementedException();
    }

    public override ShaderModule CreateShaderModuleFromFile(string path, ShaderStage stage, string entryPoint)
    {
        throw new NotImplementedException();
    }

    public override Shader CreateShader(in ReadOnlySpan<ShaderAttachment> attachments)
    {
        throw new NotImplementedException();
    }

    public override InputLayout CreateInputLayout(in ReadOnlySpan<InputLayoutDescription> descriptions, ShaderModule vertexModule)
    {
        throw new NotImplementedException();
    }

    public override void SetPrimitiveType(PrimitiveType type)
    {
        throw new NotImplementedException();
    }

    public override void SetShader(Shader shader)
    {
        throw new NotImplementedException();
    }

    public override void SetInputLayout(InputLayout layout)
    {
        throw new NotImplementedException();
    }

    public override void SetVertexBuffer(uint slot, GraphicsBuffer buffer, uint stride)
    {
        throw new NotImplementedException();
    }

    public override void SetIndexBuffer(GraphicsBuffer buffer, Format format)
    {
        throw new NotImplementedException();
    }

    public override void Draw(uint vertexCount)
    {
        throw new NotImplementedException();
    }

    public override void DrawIndexed(uint indexCount)
    {
        throw new NotImplementedException();
    }

    public override void Present()
    {
        _context.Present(1);
    }

    public override void ResizeSwapchain(in Size<int> size)
    {
        throw new NotImplementedException();
    }

    public override void Dispose()
    {
        _gl.Dispose();
    }
}
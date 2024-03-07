using Silk.NET.OpenGL;
using u4.Math;

namespace u4.Render.Backend.GL45;

public unsafe class GL45GraphicsDevice : GraphicsDevice
{
    private GLContext _context;
    
    public GL Gl;
    
    public override GraphicsApi Api => GraphicsApi.OpenGL45;
    
    public override Viewport Viewport { get; set; }

    public GL45GraphicsDevice(GLContext context, in Size<uint> size)
    {
        _context = context;

        Gl = GL.GetApi(_context.GetProcAddressFunc);
        
        Gl.Viewport(0, 0, size.Width, size.Height);
    }
    
    public override void ClearColorBuffer(Color color)
    {
        Gl.ClearColor(color.R, color.G, color.B, color.A);
        Gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    public override GraphicsBuffer CreateBuffer<T>(in BufferDescription description, in ReadOnlySpan<T> data)
    {
        fixed (void* pData = data)
            return new GL45GraphicsBuffer(Gl, description, pData);
    }

    public override ShaderModule CreateShaderModuleFromFile(string path, ShaderStage stage, string entryPoint)
    {
        return new GL45ShaderModule(Gl, File.ReadAllText(path), stage);
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

    public override void ResizeSwapchain(in Size<int> size) { }

    public override void Dispose()
    {
        Gl.Dispose();
    }
}
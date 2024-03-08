using Silk.NET.OpenGL;
using u4.Math;

namespace u4.Render.Backend.GL45;

public sealed unsafe class GL45GraphicsDevice : GraphicsDevice
{
    private GLContext _context;
    private Size<uint> _currentSize;
    private Silk.NET.OpenGL.PrimitiveType _primitiveType;
    private Viewport _viewport;
    
    public GL Gl;
    
    public override GraphicsApi Api => GraphicsApi.OpenGL45;

    public override Viewport Viewport
    {
        get => _viewport;
        set
        {
            _viewport = value;
            
            // Convert D3D top-left viewport to GL viewport.
            Gl.Viewport(value.X,(int) _currentSize.Height - (int) value.Height - value.Y, value.Width, value.Height);
        }
    }

    public GL45GraphicsDevice(GLContext context, in Size<uint> size)
    {
        _context = context;
        _currentSize = size;

        Gl = GL.GetApi(_context.GetProcAddressFunc);

        Viewport = new Viewport(0, 0, size.Width, size.Height);
        
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
        return new GL45Shader(Gl, attachments);
    }

    public override InputLayout CreateInputLayout(in ReadOnlySpan<InputLayoutDescription> descriptions, ShaderModule vertexModule)
    {
        return new GL45InputLayout(Gl, descriptions);
    }

    public override void SetPrimitiveType(PrimitiveType type)
    {
        _primitiveType = type switch
        {
            PrimitiveType.TriangleList => Silk.NET.OpenGL.PrimitiveType.Triangles,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public override void SetShader(Shader shader)
    {
        GL45Shader glShader = (GL45Shader) shader;
        Gl.UseProgram(glShader.Program);
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
        Gl.DrawArrays(_primitiveType, 0, vertexCount);
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
        _currentSize = size.As<uint>();
        // To make behaviour consistent with D3D, we set the viewport to itself to ensure it stays at the top left.
        Viewport = _viewport;
    }

    public override void Dispose()
    {
        Gl.Dispose();
    }
}
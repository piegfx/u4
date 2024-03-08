using Silk.NET.OpenGL;
using u4.Math;

namespace u4.Render.Backend.GL45;

public sealed unsafe class GL45GraphicsDevice : GraphicsDevice
{
    private GLContext _context;
    private Size<uint> _currentSize;
    private Silk.NET.OpenGL.PrimitiveType _primitiveType;
    private DrawElementsType _drawElementsType;
    private Viewport _viewport;

    private (uint buffer, uint stride)[] _boundVbos;
    private uint _currentEbo;
    private uint _currentVao;
    
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

        // Loosely based on the maximum number D3D provides, can't find a concrete value for OpenGL.
        _boundVbos = new (uint, uint)[16];

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
        GL45InputLayout glLayout = (GL45InputLayout) layout;
        _currentVao = glLayout.Vao;
    }

    public override void SetVertexBuffer(uint slot, GraphicsBuffer buffer, uint stride)
    {
        GL45GraphicsBuffer glBuffer = (GL45GraphicsBuffer) buffer;

        _boundVbos[slot] = (glBuffer.Buffer, stride);
    }

    public override void SetIndexBuffer(GraphicsBuffer buffer, Format format)
    {
        _drawElementsType = format switch
        {
            Format.R32UInt => DrawElementsType.UnsignedInt,
            Format.R16UInt => DrawElementsType.UnsignedShort,
            Format.R8UInt => DrawElementsType.UnsignedByte,
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
        
        GL45GraphicsBuffer glBuffer = (GL45GraphicsBuffer) buffer;

        _currentEbo = glBuffer.Buffer;
    }

    public override void Draw(uint vertexCount)
    {
        BindBuffersToVao();
        Gl.DrawArrays(_primitiveType, 0, vertexCount);
    }

    public override void DrawIndexed(uint indexCount)
    {
        BindBuffersToVao();
        Gl.DrawElements(_primitiveType, indexCount, _drawElementsType, (void*) 0);
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

    private void BindBuffersToVao()
    {
        Gl.BindVertexArray(_currentVao);

        for (uint i = 0; i < _boundVbos.Length; i++)
        {
            if (_boundVbos[i].buffer == 0)
                continue;
            
            Gl.VertexArrayVertexBuffer(_currentVao, i, _boundVbos[i].buffer, IntPtr.Zero, _boundVbos[i].stride);
        }

        Gl.VertexArrayElementBuffer(_currentVao, _currentEbo);
    }
}
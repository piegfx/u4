using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Pie;
using Pie.ShaderCompiler;
using u4.Math;

namespace u4.Render.Renderers;

public sealed class SpriteRenderer : IDisposable
{
    public const uint MaxSprites = 2048;

    private const uint NumVertices = 4;
    private const uint NumIndices = 6;

    private const uint MaxVertices = NumVertices * MaxSprites;
    private const uint MaxIndices = NumIndices * MaxSprites;

    private readonly GraphicsDevice _device;

    private readonly Vertex[] _vertices;
    private readonly uint[] _indices;
    
    private readonly GraphicsBuffer _vertexBuffer;
    private readonly GraphicsBuffer _indexBuffer;

    private readonly GraphicsBuffer _transformBuffer;

    private Shader _shader;
    private InputLayout _inputLayout;

    private DepthStencilState _depthStencilState;
    private RasterizerState _rasterizerState;
    private BlendState _blendState;
    private SamplerState _samplerState;

    private bool _isBegun;
    private Texture _texture;
    private uint _currentSprite;

    public bool IsBegun => _isBegun;

    public SpriteRenderer(GraphicsDevice device)
    {
        _device = device;

        _vertices = new Vertex[MaxVertices];
        _indices = new uint[MaxIndices];

        _vertexBuffer = device.CreateBuffer(BufferType.VertexBuffer, MaxVertices * Vertex.SizeInBytes, true);
        _indexBuffer = device.CreateBuffer(BufferType.IndexBuffer, MaxVertices * sizeof(uint), true);

        _transformBuffer = device.CreateBuffer(BufferType.UniformBuffer, Matrix4x4.Identity, true);

        byte[] vertBytes = File.ReadAllBytes("Content/Shaders/Sprite/Sprite_vert.spv");
        byte[] fragBytes = File.ReadAllBytes("Content/Shaders/Sprite/Sprite_frag.spv");

        _shader = device.CreateShader(new[]
        {
            new ShaderAttachment(ShaderStage.Vertex, vertBytes, "Vertex"),
            new ShaderAttachment(ShaderStage.Pixel, fragBytes, "Pixel")
        });

        _inputLayout = device.CreateInputLayout(new[]
        {
            new InputLayoutDescription(Format.R32G32_Float, 0, 0, InputType.PerVertex), // Position
            new InputLayoutDescription(Format.R32G32_Float, 8, 0, InputType.PerVertex), // TexCoord
            new InputLayoutDescription(Format.R32G32B32A32_Float, 16, 0, InputType.PerVertex) // Tint
        });

        _depthStencilState = device.CreateDepthStencilState(DepthStencilStateDescription.Disabled);
        _rasterizerState = device.CreateRasterizerState(RasterizerStateDescription.CullNone);
        _blendState = device.CreateBlendState(BlendStateDescription.NonPremultiplied);
        _samplerState = device.CreateSamplerState(SamplerStateDescription.LinearClamp);
    }

    public void Begin(Matrix4x4? transform = null, Matrix4x4? projection = null)
    {
        if (_isBegun)
            throw new Exception("Cannot begin, sprite renderer has already begun.");

        _isBegun = true;
        
        System.Drawing.Rectangle viewport = _device.Viewport;
        Matrix4x4 proj = projection ?? Matrix4x4.CreateOrthographicOffCenter(viewport.X, viewport.X + viewport.Width,
            viewport.Y + viewport.Height, viewport.Y, -1, 1);
        Matrix4x4 trans = transform ?? Matrix4x4.Identity;
        
        _device.UpdateBuffer(_transformBuffer, 0, trans * proj);
    }

    public void End()
    {
        if (!_isBegun)
            throw new Exception("Cannot end, sprite renderer has not begun.");

        _isBegun = false;
        Flush();
    }
    
    public void Draw(Texture texture, Vector2 position)
        => Draw(texture, position, Color.White, 0, Vector2.One, Vector2.Zero);

    public void Draw(Texture texture, Vector2 position, Color tint, float rotation, Vector2 scale, Vector2 origin)
    {
        Size<int> size = texture.Size;

        Matrix4x4 transform = Matrix4x4.CreateRotationZ(rotation);

        Vector2 topLeft = position + Vector2.Transform(-origin * scale, transform);
        Vector2 topRight = position + Vector2.Transform(-origin * scale + new Vector2(size.Width, 0) * scale, transform);
        Vector2 bottomLeft = position + Vector2.Transform(-origin * scale + new Vector2(0, size.Height) * scale, transform);
        Vector2 bottomRight = position + Vector2.Transform(-origin * scale + new Vector2(size.Width, size.Height) * scale, transform);
        
        Draw(texture, topLeft, topRight, bottomLeft, bottomRight, tint);
    }

    public void Draw(Texture texture, Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight,
        Color tint)
    {
        if (!_isBegun)
            throw new Exception("Cannot draw, sprite renderer has not begun.");
        
        if (_texture != texture || _currentSprite >= MaxSprites)
            Flush();

        _texture = texture;

        uint vOffset = _currentSprite * NumVertices;
        uint iOffset = _currentSprite * NumIndices;

        _vertices[vOffset + 0] = new Vertex(topLeft, new Vector2(0, 0), tint);
        _vertices[vOffset + 1] = new Vertex(topRight, new Vector2(1, 0), tint);
        _vertices[vOffset + 2] = new Vertex(bottomRight, new Vector2(1, 1), tint);
        _vertices[vOffset + 3] = new Vertex(bottomLeft, new Vector2(0, 1), tint);

        _indices[iOffset + 0] = 0 + vOffset;
        _indices[iOffset + 1] = 1 + vOffset;
        _indices[iOffset + 2] = 3 + vOffset;
        _indices[iOffset + 3] = 1 + vOffset;
        _indices[iOffset + 4] = 2 + vOffset;
        _indices[iOffset + 5] = 3 + vOffset;

        _currentSprite++;
    }

    private void Flush()
    {
        // No need to flush if there is nothing to do.
        if (_currentSprite == 0)
            return;

        MappedSubresource vRes = _device.MapResource(_vertexBuffer, MapMode.Write);
        PieUtils.CopyToUnmanaged(vRes.DataPtr, 0, _currentSprite * NumVertices * Vertex.SizeInBytes, _vertices);
        _device.UnmapResource(_vertexBuffer);

        MappedSubresource iRes = _device.MapResource(_indexBuffer, MapMode.Write);
        PieUtils.CopyToUnmanaged(iRes.DataPtr, 0, _currentSprite * NumIndices * sizeof(uint), _indices);
        _device.UnmapResource(_indexBuffer);
        
        _device.SetPrimitiveType(PrimitiveType.TriangleList);
        _device.SetShader(_shader);
        
        _device.SetDepthStencilState(_depthStencilState);
        _device.SetBlendState(_blendState);
        _device.SetRasterizerState(_rasterizerState);
        
        _device.SetUniformBuffer(0, _transformBuffer);
        _device.SetTexture(1, _texture.PieTexture, _samplerState);
        
        _device.SetInputLayout(_inputLayout);
        _device.SetVertexBuffer(0, _vertexBuffer, Vertex.SizeInBytes);
        _device.SetIndexBuffer(_indexBuffer, IndexType.UInt);
        
        _device.DrawIndexed(NumIndices * _currentSprite);

        _currentSprite = 0;
    }
    
    public void Dispose()
    {
        _samplerState.Dispose();
        _blendState.Dispose();
        _rasterizerState.Dispose();
        _device.Dispose();
        
        _inputLayout.Dispose();
        _shader.Dispose();
        
        _vertexBuffer.Dispose();
        _indexBuffer.Dispose();
        _transformBuffer.Dispose();
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Vertex
    {
        public Vector2 Position;
        public Vector2 TexCoord;
        public Color Tint;

        public Vertex(Vector2 position, Vector2 texCoord, Color tint)
        {
            Position = position;
            TexCoord = texCoord;
            Tint = tint;
        }

        public const uint SizeInBytes = 32;
    }
}
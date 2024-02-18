using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Pie;
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

    public SpriteRenderer(GraphicsDevice device)
    {
        _device = device;

        _vertices = new Vertex[MaxVertices];
        _indices = new uint[MaxIndices];

        _vertexBuffer = device.CreateBuffer(BufferType.VertexBuffer, MaxVertices * Vertex.SizeInBytes, true);
        _indexBuffer = device.CreateBuffer(BufferType.IndexBuffer, MaxVertices * sizeof(uint), true);
    }
    
    public void Dispose()
    {
        _vertexBuffer.Dispose();
        _indexBuffer.Dispose();
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Vertex
    {
        public Vector2 Position;
        public Vector2 TexCoord;
        public Color Tint;

        public const uint SizeInBytes = 32;
    }
}
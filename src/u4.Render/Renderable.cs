using System;
using Pie;

namespace u4.Render;

public class Renderable : IDisposable
{
    public readonly GraphicsBuffer VertexBuffer;
    public readonly GraphicsBuffer IndexBuffer;

    public readonly uint NumElements;
    
    public Renderable(GraphicsBuffer vertexBuffer, GraphicsBuffer indexBuffer, uint numElements)
    {
        VertexBuffer = vertexBuffer;
        IndexBuffer = indexBuffer;
        NumElements = numElements;
    }

    public Renderable(Vertex[] vertices, uint[] indices, bool dynamic = false)
    {
        GraphicsDevice device = Graphics.Device;

        VertexBuffer = device.CreateBuffer(BufferType.VertexBuffer, vertices, dynamic);
        IndexBuffer = device.CreateBuffer(BufferType.IndexBuffer, indices, dynamic);

        NumElements = (uint) indices.Length;
    }
    
    public void Dispose()
    {
        VertexBuffer.Dispose();
        IndexBuffer.Dispose();
    }
}
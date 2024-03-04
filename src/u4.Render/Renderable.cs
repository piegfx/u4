using System;
using Pie;

namespace u4.Render;

public class Renderable : IDisposable
{
    public readonly GraphicsBuffer VertexBuffer;
    public readonly GraphicsBuffer IndexBuffer;

    public readonly uint NumElements;

    public Material Material;
    
    public Renderable(GraphicsBuffer vertexBuffer, GraphicsBuffer indexBuffer, uint numElements, Material material)
    {
        VertexBuffer = vertexBuffer;
        IndexBuffer = indexBuffer;
        NumElements = numElements;
        Material = material;
    }

    public Renderable(Vertex[] vertices, uint[] indices, Material material, bool dynamic = false)
    {
        GraphicsDevice device = Graphics.Device;

        VertexBuffer = device.CreateBuffer(BufferType.VertexBuffer, vertices, dynamic);
        IndexBuffer = device.CreateBuffer(BufferType.IndexBuffer, indices, dynamic);

        NumElements = (uint) indices.Length;

        Material = material;
    }
    
    public void Dispose()
    {
        VertexBuffer.Dispose();
        IndexBuffer.Dispose();
    }
}
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D11_BIND_FLAG;
using static TerraFX.Interop.DirectX.D3D11_CPU_ACCESS_FLAG;
using static TerraFX.Interop.DirectX.D3D11_USAGE;
using static TerraFX.Interop.Windows.Windows;

namespace u4.Render.Backend.D3D11;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal sealed unsafe class D3D11GraphicsBuffer : GraphicsBuffer
{
    public readonly ID3D11Buffer* Buffer;
    public readonly bool Dynamic;
    
    public override BufferType Type { get; }

    public D3D11GraphicsBuffer(ID3D11Device* device, in BufferDescription description, void* data)
    {
        Type = description.Type;
        Dynamic = description.Dynamic;
        
        D3D11_BIND_FLAG bindFlag = description.Type switch
        {
            BufferType.Vertex => D3D11_BIND_VERTEX_BUFFER,
            BufferType.Index => D3D11_BIND_INDEX_BUFFER,
            BufferType.Constant => D3D11_BIND_CONSTANT_BUFFER,
            _ => throw new ArgumentOutOfRangeException()
        };

        D3D11_BUFFER_DESC bufferDesc = new()
        {
            BindFlags = (uint)bindFlag,
            ByteWidth = description.SizeInBytes,
            Usage = description.Dynamic ? D3D11_USAGE_DYNAMIC : D3D11_USAGE_DEFAULT,
            CPUAccessFlags = description.Dynamic ? (uint)D3D11_CPU_ACCESS_WRITE : 0
        };

        D3D11_SUBRESOURCE_DATA subData = new()
        {
            pSysMem = data
        };

        ID3D11Buffer* buffer;
        if (FAILED(device->CreateBuffer(&bufferDesc, data == null ? null : &subData, &buffer)))
            throw new Exception("Failed to create buffer.");

        Buffer = buffer;
    }

    public override void Dispose()
    {
        Buffer->Release();
    }
}
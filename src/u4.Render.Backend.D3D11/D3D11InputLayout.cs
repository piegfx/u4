using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.Windows.Windows;

namespace u4.Render.Backend.D3D11;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal sealed unsafe class D3D11InputLayout : InputLayout
{
    public ID3D11InputLayout* Layout;

    public D3D11InputLayout(ID3D11Device* device, in ReadOnlySpan<InputLayoutDescription> descriptions, ID3DBlob* blob)
    {
        D3D11_INPUT_ELEMENT_DESC* elementDescs = stackalloc D3D11_INPUT_ELEMENT_DESC[descriptions.Length];
        // TODO: Is there a better way to do this?
        GCHandle* semanticHandles = stackalloc GCHandle[descriptions.Length];

        for (int i = 0; i < descriptions.Length; i++)
        {
            InputLayoutDescription desc = descriptions[i];
            
            semanticHandles[i] = GCHandle.Alloc(Encoding.UTF8.GetBytes(desc.SemanticName), GCHandleType.Pinned);
            
            elementDescs[i] = new D3D11_INPUT_ELEMENT_DESC()
            {
                SemanticName = (sbyte*) semanticHandles[i].AddrOfPinnedObject(),
                SemanticIndex = desc.SemanticIndex,
                Format = desc.Format.ToDxgiFormat(),
                AlignedByteOffset = desc.Offset,
                InputSlot = desc.Slot,
                InputSlotClass = desc.Type.ToInputClassification(),
                InstanceDataStepRate = (uint) desc.Type
            };
        }

        ID3D11InputLayout* layout;
        if (FAILED(device->CreateInputLayout(elementDescs, (uint) descriptions.Length, blob->GetBufferPointer(),
                blob->GetBufferSize(), &layout)))
        {
            throw new Exception("Failed to create input layout.");
        }
        
        Layout = layout;
        
        for (int i = 0; i < descriptions.Length; i++)
            semanticHandles[i].Free();
    }
    
    public override void Dispose()
    {
        Layout->Release();
    }
}
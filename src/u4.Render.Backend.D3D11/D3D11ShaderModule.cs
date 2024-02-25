using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop.DirectX;

namespace u4.Render.Backend.D3D11;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public sealed unsafe class D3D11ShaderModule : ShaderModule
{
    public readonly ID3DBlob* Blob;
    public readonly ShaderStage Stage;
    
    public D3D11ShaderModule(ID3DBlob* blob, ShaderStage stage)
    {
        Blob = blob;
        Stage = stage;
    }

    public override void Dispose()
    {
        Blob->Release();
    }
}
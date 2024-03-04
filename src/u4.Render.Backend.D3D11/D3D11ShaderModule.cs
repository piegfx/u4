using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop.DirectX;
using u4.Render.Backend.Exceptions;
using static TerraFX.Interop.DirectX.D3D;
using static TerraFX.Interop.DirectX.DirectX;
using static TerraFX.Interop.Windows.Windows;

namespace u4.Render.Backend.D3D11;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal sealed unsafe class D3D11ShaderModule : ShaderModule
{
    public readonly ID3DBlob* Blob;
    public readonly ShaderStage Stage;
    
    public D3D11ShaderModule(ID3DBlob* blob, ShaderStage stage)
    {
        Blob = blob;
        Stage = stage;
    }

    public static D3D11ShaderModule FromFile(in ReadOnlySpan<char> path, ShaderStage stage, in ReadOnlySpan<byte> entryPoint)
    {
        ID3DBlob* result;
        ID3DBlob* error;
        
        ReadOnlySpan<byte> profile = stage switch
        {
            ShaderStage.Vertex => "vs_5_0"u8,
            ShaderStage.Pixel => "ps_5_0"u8,
            _ => throw new ArgumentOutOfRangeException(nameof(stage), stage, null)
        };
        
        fixed (char* pPath = path)
        fixed (byte* pEntryPoint = entryPoint)
        fixed (byte* pProfile = profile)
        {
            if (FAILED(D3DCompileFromFile(pPath, null, D3D_COMPILE_STANDARD_FILE_INCLUDE, (sbyte*) pEntryPoint, (sbyte*) pProfile, 0, 0, &result, &error)))
            {
                throw new ShaderCompilationException(stage,
                    new string((sbyte*) error->GetBufferPointer(), 0, (int) error->GetBufferSize()));
            }
        }

        if (error != null)
            error->Release();

        return new D3D11ShaderModule(result, stage);
    }

    public override void Dispose()
    {
        Blob->Release();
    }
}
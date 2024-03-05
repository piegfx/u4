using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.Windows.Windows;

namespace u4.Render.Backend.D3D11;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal sealed unsafe class D3D11Shader : Shader
{
    public ShaderObject[] Objects;

    public D3D11Shader(ID3D11Device* device, in ReadOnlySpan<ShaderAttachment> attachments)
    {
        Objects = new ShaderObject[attachments.Length];

        for (int i = 0; i < attachments.Length; i++)
        {
            D3D11ShaderModule module = (D3D11ShaderModule) attachments[i].Module;
            ID3DBlob* blob = module.Blob;

            switch (attachments[i].Stage)
            {
                case ShaderStage.Vertex:
                    ID3D11VertexShader* vShader;
                    if (FAILED(device->CreateVertexShader(blob->GetBufferPointer(), blob->GetBufferSize(), null, &vShader)))
                        throw new Exception("Failed to create vertex shader");

                    Objects[i] = new ShaderObject((ID3D11DeviceChild*) vShader, attachments[i].Stage);
                    
                    break;
                case ShaderStage.Pixel:
                    ID3D11PixelShader* pShader;
                    if (FAILED(device->CreatePixelShader(blob->GetBufferPointer(), blob->GetBufferSize(), null, &pShader)))
                        throw new Exception("Failed to create pixel shader.");

                    Objects[i] = new ShaderObject((ID3D11DeviceChild*) pShader, attachments[i].Stage);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    public override void Dispose()
    {
        for (int i = 0; i < Objects.Length; i++)
            Objects[i].Shader->Release();
    }

    public struct ShaderObject
    {
        public ID3D11DeviceChild* Shader;
        public ShaderStage Stage;

        public ShaderObject(ID3D11DeviceChild* shader, ShaderStage stage)
        {
            Shader = shader;
            Stage = stage;
        }
    }
}
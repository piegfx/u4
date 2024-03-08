using Silk.NET.OpenGL;

namespace u4.Render.Backend.GL45;

internal sealed class GL45InputLayout : InputLayout
{
    private GL _gl;

    public uint Vao;

    public GL45InputLayout(GL gl, in ReadOnlySpan<InputLayoutDescription> descriptions)
    {
        _gl = gl;
        
        Vao = gl.CreateVertexArray();

        for (int i = 0; i < descriptions.Length; i++)
        {
            gl.EnableVertexArrayAttrib(Vao, (uint) i);
            gl.VertexArrayAttribBinding(Vao, (uint) i, descriptions[i].Slot);
            
            switch (descriptions[i].Format)
            {
                case Format.R1UNorm:
                    throw new NotImplementedException();
                case Format.A8UNorm:
                    throw new NotImplementedException();
                case Format.R8UNorm:
                    throw new NotImplementedException();
                case Format.R8UInt:
                    throw new NotImplementedException();
                case Format.R8SNorm:
                    throw new NotImplementedException();
                case Format.R8SInt:
                    throw new NotImplementedException();
                case Format.R8G8UNorm:
                    throw new NotImplementedException();
                case Format.R8G8UInt:
                    throw new NotImplementedException();
                case Format.R8G8SNorm:
                    throw new NotImplementedException();
                case Format.R8G8SInt:
                    throw new NotImplementedException();
                case Format.R8G8B8A8UNorm:
                    throw new NotImplementedException();
                case Format.R8G8B8A8UNormSRGB:
                    throw new NotImplementedException();
                case Format.R8G8B8A8UInt:
                    throw new NotImplementedException();
                case Format.R8G8B8A8SNorm:
                    throw new NotImplementedException();
                case Format.R8G8B8A8SInt:
                    throw new NotImplementedException();
                case Format.B8G8R8A8UNorm:
                    throw new NotImplementedException();
                case Format.B8G8R8A8UNormSRGB:
                    throw new NotImplementedException();
                case Format.R16Float:
                    throw new NotImplementedException();
                case Format.R16UNorm:
                    throw new NotImplementedException();
                case Format.R16UInt:
                    throw new NotImplementedException();
                case Format.R16SNorm:
                    throw new NotImplementedException();
                case Format.R16SInt:
                    throw new NotImplementedException();
                case Format.R16G16Float:
                    throw new NotImplementedException();
                case Format.R16G16UNorm:
                    throw new NotImplementedException();
                case Format.R16G16UInt:
                    throw new NotImplementedException();
                case Format.R16G16SNorm:
                    throw new NotImplementedException();
                case Format.R16G16SInt:
                    throw new NotImplementedException();
                case Format.R16G16B16A16Float:
                    throw new NotImplementedException();
                case Format.R16G16B16A16UNorm:
                    throw new NotImplementedException();
                case Format.R16G16B16A16UInt:
                    throw new NotImplementedException();
                case Format.R16G16B16A16SNorm:
                    throw new NotImplementedException();
                case Format.R16G16B16A16SInt:
                    throw new NotImplementedException();
                case Format.R32Float:
                    throw new NotImplementedException();
                case Format.R32UInt:
                    throw new NotImplementedException();
                case Format.R32SInt:
                    throw new NotImplementedException();
                case Format.R32G32Float:
                    throw new NotImplementedException();
                case Format.R32G32UInt:
                    throw new NotImplementedException();
                case Format.R32G32SInt:
                    throw new NotImplementedException();
                case Format.R32G32B32A32Float:
                    gl.VertexArrayAttribFormat(Vao, (uint) i, 4, VertexAttribType.Float, false, descriptions[i].Offset);
                    break;
                case Format.R32G32B32A32UInt:
                    throw new NotImplementedException();
                case Format.R32G32B32A32SInt:
                    throw new NotImplementedException();
                case Format.R32G32B32Float:
                    gl.VertexArrayAttribFormat(Vao, (uint) i, 3, VertexAttribType.Float, false, descriptions[i].Offset);
                    break;
                case Format.R32G32B32UInt:
                    throw new NotImplementedException();
                case Format.R32G32B32SInt:
                    throw new NotImplementedException();
                case Format.R10G10B10A2UNorm:
                    throw new NotImplementedException();
                case Format.R10G10B10A2UInt:
                    throw new NotImplementedException();
                case Format.R11G11B10Float:
                    throw new NotImplementedException();
                case Format.D16UNorm:
                    throw new NotImplementedException();
                case Format.D24UNormS8UInt:
                    throw new NotImplementedException();
                case Format.D32Float:
                    throw new NotImplementedException();
                case Format.BC1UNorm:
                    throw new NotImplementedException();
                case Format.BC1UNormSRGB:
                    throw new NotImplementedException();
                case Format.BC2UNorm:
                    throw new NotImplementedException();
                case Format.BC2UNormSRGB:
                    throw new NotImplementedException();
                case Format.BC3UNorm:
                    throw new NotImplementedException();
                case Format.BC3UNormSRGB:
                    throw new NotImplementedException();
                case Format.BC4UNorm:
                    throw new NotImplementedException();
                case Format.BC4SNorm:
                    throw new NotImplementedException();
                case Format.BC5UNorm:
                    throw new NotImplementedException();
                case Format.BC5SNorm:
                    throw new NotImplementedException();
                case Format.BC6HUF16:
                    throw new NotImplementedException();
                case Format.BC6HSF16:
                    throw new NotImplementedException();
                case Format.BC7UNorm:
                    throw new NotImplementedException();
                case Format.BC7UNormSRGB:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    public override void Dispose()
    {
        _gl.DeleteVertexArray(Vao);
    }
}
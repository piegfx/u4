using Silk.NET.OpenGL;
using u4.Render.Backend.Exceptions;

namespace u4.Render.Backend.GL45;

internal sealed class GL45ShaderModule : ShaderModule
{
    private GL _gl;
    
    public uint Shader;

    public GL45ShaderModule(GL gl, string shaderCode, ShaderStage stage)
    {
        _gl = gl;
        
        Shader = gl.CreateShader(stage switch
        {
            ShaderStage.Vertex => ShaderType.VertexShader,
            ShaderStage.Pixel => ShaderType.FragmentShader,
            _ => throw new ArgumentOutOfRangeException(nameof(stage), stage, null)
        });
        
        gl.ShaderSource(Shader, shaderCode);
        
        gl.CompileShader(Shader);

        gl.GetShader(Shader, ShaderParameterName.CompileStatus, out int status);
        if (status != (int) GLEnum.True)
            throw new ShaderCompilationException(stage, gl.GetShaderInfoLog(Shader));
    }
    
    public override void Dispose()
    {
        _gl.DeleteShader(Shader);
    }
}
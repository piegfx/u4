using Silk.NET.OpenGL;

namespace u4.Render.Backend.GL45;

internal sealed class GL45Shader : Shader
{
    private GL _gl;
    
    public uint Program;

    public GL45Shader(GL gl, in ReadOnlySpan<ShaderAttachment> attachments)
    {
        _gl = gl;

        Program = gl.CreateProgram();

        for (int i = 0; i < attachments.Length; i++)
        {
            GL45ShaderModule module = (GL45ShaderModule) attachments[i].Module;
            gl.AttachShader(Program, module.Shader);
        }
        
        gl.LinkProgram(Program);

        gl.GetProgram(Program, ProgramPropertyARB.LinkStatus, out int status);
        if (status != (int) GLEnum.True)
            throw new Exception("Failed to link program: " + gl.GetProgramInfoLog(Program));
        
        for (int i = 0; i < attachments.Length; i++)
        {
            GL45ShaderModule module = (GL45ShaderModule) attachments[i].Module;
            gl.DetachShader(Program, module.Shader);
        }
    }
    
    public override void Dispose()
    {
        _gl.DeleteProgram(Program);
    }
}
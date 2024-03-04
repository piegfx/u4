namespace u4.Render.Backend.Exceptions;

public class ShaderCompilationException : Exception
{
    public ShaderCompilationException(ShaderStage stage, string errorMsg) : base(
        $"Failed to compile {stage} shader: {errorMsg}") { }
}
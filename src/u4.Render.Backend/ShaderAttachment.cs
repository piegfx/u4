namespace u4.Render.Backend;

public readonly struct ShaderAttachment
{
    public readonly ShaderModule Module;
    public readonly ShaderStage Stage;

    public ShaderAttachment(ShaderModule module, ShaderStage stage)
    {
        Module = module;
        Stage = stage;
    }
}
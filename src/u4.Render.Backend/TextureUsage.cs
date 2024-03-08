namespace u4.Render.Backend;

[Flags]
public enum TextureUsage
{
    None,
    
    ShaderResource,
    
    RenderTarget,
    
    GenerateMipmaps
}
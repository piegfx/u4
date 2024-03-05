namespace u4.Render.Backend;

/// <summary>
/// Represents supported data formats. Used in textures and input layouts, among other things. Based on DXGI_FORMAT, but
/// not cast-compatible.
/// </summary>
public enum Format
{
    R8UNorm,
    R8UInt,
    R8SNorm,
    R8SInt,
    
    R16UNorm,
    R16UInt,
    R16SNorm,
    R16SInt,
    R16Float,
    
    
}
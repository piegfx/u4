namespace u4.Render.Backend;

/// <summary>
/// Represents supported data formats. Used in textures and input layouts, among other things. Based on DXGI_FORMAT, but
/// not cast-compatible.
/// </summary>
public enum Format
{
    R1UNorm,
    A8UNorm,
    
    R8UNorm,
    R8UInt,
    R8SNorm,
    R8SInt,
    
    R8G8UNorm,
    R8G8UInt,
    R8G8SNorm,
    R8G8SInt,
    
    R8G8B8A8UNorm,
    R8G8B8A8UNormSRGB,
    R8G8B8A8UInt,
    R8G8B8A8SNorm,
    R8G8B8A8SInt,
    
    B8G8R8A8UNorm,
    B8G8R8A8UNormSRGB,
    
    R16Float,
    R16UNorm,
    R16UInt,
    R16SNorm,
    R16SInt,
    
    R16G16Float,
    R16G16UNorm,
    R16G16UInt,
    R16G16SNorm,
    R16G16SInt,
    
    R16G16B16A16Float,
    R16G16B16A16UNorm,
    R16G16B16A16UInt,
    R16G16B16A16SNorm,
    R16G16B16A16SInt,
    
    R32Float,
    R32UInt,
    R32SInt,
    
    R32G32Float,
    R32G32UInt,
    R32G32SInt,
    
    R32G32B32A32Float,
    R32G32B32A32UInt,
    R32G32B32A32SInt,
    R32G32B32Float,
    R32G32B32UInt,
    R32G32B32SInt,
    
    R10G10B10A2UNorm,
    R10G10B10A2UInt,
    R11G11B10Float,
    
    D16UNorm,
    D24UNormS8UInt,
    D32Float,
    
    BC1UNorm,
    BC1UNormSRGB,
    
    BC2UNorm,
    BC2UNormSRGB,
    
    BC3UNorm,
    BC3UNormSRGB,
    
    BC4UNorm,
    BC4SNorm,
    
    BC5UNorm,
    BC5SNorm,
    
    BC6HUF16,
    BC6HSF16,
    
    BC7UNorm,
    BC7UNormSRGB
}
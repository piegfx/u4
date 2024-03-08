namespace u4.Render.Backend;

public static class GraphicsUtils
{
    public static uint BitsPerPixel(this Format format)
    {
        switch (format)
        {
            case Format.R1UNorm:
                return 1;
            
            case Format.BC1UNorm:
            case Format.BC1UNormSRGB:
            case Format.BC4UNorm:
            case Format.BC4SNorm:
                return 4;
            
            case Format.A8UNorm:
            case Format.R8UNorm:
            case Format.R8UInt:
            case Format.R8SNorm:
            case Format.R8SInt:
            case Format.BC2UNorm:
            case Format.BC2UNormSRGB:
            case Format.BC3UNorm:
            case Format.BC3UNormSRGB:
            case Format.BC5UNorm:
            case Format.BC5SNorm:
            case Format.BC6HUF16:
            case Format.BC6HSF16:
            case Format.BC7UNorm:
            case Format.BC7UNormSRGB:
                return 8;
            
            case Format.R8G8UNorm:
            case Format.R8G8UInt:
            case Format.R8G8SNorm:
            case Format.R8G8SInt:
            case Format.R16Float:
            case Format.R16UNorm:
            case Format.R16UInt:
            case Format.R16SNorm:
            case Format.D16UNorm:
                return 16;
                
            case Format.R8G8B8A8UNorm:
            case Format.R8G8B8A8UNormSRGB:
            case Format.R8G8B8A8UInt:
            case Format.R8G8B8A8SNorm:
            case Format.R8G8B8A8SInt:
            case Format.B8G8R8A8UNorm:
            case Format.B8G8R8A8UNormSRGB:
            case Format.R16SInt:
            case Format.R16G16Float:
            case Format.R16G16UNorm:
            case Format.R16G16UInt:
            case Format.R16G16SNorm:
            case Format.R16G16SInt:
            case Format.R32Float:
            case Format.R32UInt:
            case Format.R32SInt:
            case Format.R10G10B10A2UNorm:
            case Format.R10G10B10A2UInt:
            case Format.R11G11B10Float:
            case Format.D24UNormS8UInt:
            case Format.D32Float:
                return 32;
            
            case Format.R16G16B16A16Float:
            case Format.R16G16B16A16UNorm:
            case Format.R16G16B16A16UInt:
            case Format.R16G16B16A16SNorm:
            case Format.R16G16B16A16SInt:
            case Format.R32G32Float:
            case Format.R32G32UInt:
            case Format.R32G32SInt:
                return 64;
            
            case Format.R32G32B32Float:
            case Format.R32G32B32UInt:
            case Format.R32G32B32SInt:
                return 96;
            
            case Format.R32G32B32A32Float:
            case Format.R32G32B32A32UInt:
            case Format.R32G32B32A32SInt:
                return 128;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }
    }
}
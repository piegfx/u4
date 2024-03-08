namespace u4.Render.Backend;

public struct TextureDescription
{
    public TextureType Type;
    
    public uint Width;
    
    public uint Height;
    
    public Format Format;
    
    public uint MipLevels;
    
    public uint ArraySize;
    
    public TextureUsage Usage;

    public TextureDescription(TextureType type, uint width, uint height, Format format, uint mipLevels, uint arraySize, TextureUsage usage)
    {
        Type = type;
        Width = width;
        Height = height;
        Format = format;
        MipLevels = mipLevels;
        ArraySize = arraySize;
        Usage = usage;
    }

    public static TextureDescription Texture2D(uint width, uint height, Format format, uint mipLevels, uint arraySize, TextureUsage usage)
        => new TextureDescription(TextureType.Texture2D, width, height, format, mipLevels, arraySize, usage);
}
using System;
using Pie;
using u4.Math;

namespace u4.Render;

public class Texture : IDisposable
{
    public readonly Pie.Texture PieTexture;

    public readonly Size<int> Size;

    public Texture(string path) : this(new Bitmap(path)) { }

    public Texture(Bitmap bitmap) : this(bitmap.Data, bitmap.Size, bitmap.Format) { }

    public Texture(byte[] data, Size<int> size, Format format = Format.R8G8B8A8_UNorm)
    {
        Size = size;
        
        GraphicsDevice device = Graphics.Device;

        TextureDescription description =
            TextureDescription.Texture2D(size.Width, size.Height, format, 0, 1, TextureUsage.ShaderResource);

        PieTexture = device.CreateTexture(description, data);
        
        device.GenerateMipmaps(PieTexture);
    }

    public Texture(Pie.Texture pieTexture)
    {
        PieTexture = pieTexture;
    }

    ~Texture()
    {
        Dispose();
    }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
        PieTexture.Dispose();
    }
}
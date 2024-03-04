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

    public Texture(Pie.Texture pieTexture, Size<int> size)
    {
        PieTexture = pieTexture;
        Size = size;
    }

    ~Texture()
    {
        Graphics.RunOnGraphicsThread(Dispose);
    }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
        PieTexture.Dispose();
    }

    public static readonly Texture White = new Texture([255, 255, 255, 255], new Size<int>(1));

    public static readonly Texture Black = new Texture([0, 0, 0, 255], new Size<int>(1));

    public static readonly Texture EmptyNormal = new Texture([128, 128, 255, 255], new Size<int>(1));

    public static readonly Texture Transparent = new Texture([0, 0, 0, 0], new Size<int>(1));
}
using System.IO;
using Pie;
using StbImageSharp;
using u4.Math;

namespace u4.Render;

public class Bitmap
{
    public readonly byte[] Data;

    public readonly Size<int> Size;

    public readonly Format Format;

    public Bitmap(string path)
    {
        using Stream stream = File.OpenRead(path);
        ImageResult result = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

        Data = result.Data;
        Size = new Size<int>(result.Width, result.Height);
        Format = Format.R8G8B8A8_UNorm;
    }

    public Bitmap(byte[] data, Size<int> size, Format format)
    {
        Data = data;
        Size = size;
        Format = format;
    }
}
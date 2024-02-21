namespace u4.Render.Backend;

public struct BufferDescription
{
    public BufferType Type;
    public uint SizeInBytes;
    public bool Dynamic;

    public BufferDescription(BufferType type, uint sizeInBytes, bool dynamic)
    {
        Type = type;
        SizeInBytes = sizeInBytes;
        Dynamic = dynamic;
    }
}
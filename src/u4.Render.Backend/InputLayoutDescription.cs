namespace u4.Render.Backend;

public struct InputLayoutDescription
{
    public string SemanticName;
    public uint SemanticIndex;
    public Format Format;
    public uint Offset;
    public uint Slot;
    public InputType Type;

    public InputLayoutDescription(string semanticName, uint semanticIndex, Format format, uint offset, uint slot, InputType type)
    {
        SemanticName = semanticName;
        SemanticIndex = semanticIndex;
        Format = format;
        Offset = offset;
        Slot = slot;
        Type = type;
    }
}
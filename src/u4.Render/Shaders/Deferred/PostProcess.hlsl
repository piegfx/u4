struct VSInput
{
    uint Index: SV_VertexID;
};

struct VSOutput
{
    float4 Position: SV_Position;
    float2 TexCoord: TEXCOORD0;
};

struct PSOutput
{
    float4 Color: SV_Target0;
};

VSOutput Vertex(const in VSInput input)
{
    VSOutput output;

    const float2 vertices[] = {
        float2(0.0, 0.0),
        float2(1.0, 0.0),
        float2(1.0, 1.0),
        float2(0.0, 1.0)
    };

    const uint indices[] = {
        0, 1, 3,
        1, 2, 3
    };

    output.Position = float4(vertices[indices[input.Index]], 0.0, 1.0);
    output.TexCoord = output.Position.xy;
    
    return output;
}

PSOutput Pixel(const in VSOutput input)
{
    PSOutput output;

    output.Color = float4(1.0, 0.5, 0.25, 1.0);

    return output;
}
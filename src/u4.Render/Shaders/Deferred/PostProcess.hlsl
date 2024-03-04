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

Texture2D AlbedoTexture : register(t0);
SamplerState State      : register(s0);

VSOutput Vertex(const in VSInput input)
{
    VSOutput output;

    const float2 vertices[] = {
        float2(-1.0, -1.0),
        float2(+1.0, -1.0),
        float2(+1.0, +1.0),
        float2(-1.0, +1.0)
    };

    const float2 texCoords[] = {
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
    output.TexCoord = texCoords[indices[input.Index]];
    
    return output;
}

PSOutput Pixel(const in VSOutput input)
{
    PSOutput output;

    float4 albedo = AlbedoTexture.Sample(State, input.TexCoord);

    // As the targets are cleared with an alpha value of 0, we only need to draw to the target if there is actually
    // an object there. Otherwise, let whatever's in the background show through.
    if (albedo.a < 0.5)
        discard;

    output.Color = float4(albedo.rgb, 1.0);

    return output;
}
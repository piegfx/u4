#include "Common.hlsli"

struct VSInput
{
    uint Index: SV_VertexID;
};

struct VSOutput
{
    float4 Position: SV_Position;
    float4 Color: COLOR0;
};

struct PSOutput
{
    float4 Color: SV_Target0;
};

VSOutput Vertex(const in VSInput input)
{
    VSOutput output;

    output.Position = float4(RectVerts[RectIndices[input.Index]], 0.0, 1.0);
    output.Color = RectColors[RectIndices[input.Index]];
    
    return output;
}

PSOutput Pixel(const in VSOutput input)
{
    PSOutput output;

    output.Color = input.Color;
    
    return output;
}
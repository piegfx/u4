#include "Common.hlsli"

struct VSInput
{
    uint Index: SV_VertexID;
};

struct VSOutput
{
    float4 Position: SV_Position;
};

struct PSOutput
{
    float4 Color: SV_Target0;
};

VSOutput Vertex(const in VSInput input)
{
    VSOutput output;

    output.Position = float4(RectVerts[RectIndices[input.Index]], 0.0, 1.0);
    
    return output;
}

PSOutput Pixel(const in VSOutput input)
{
    PSOutput output;

    output.Color = float4(1.0, 0.5, 0.25, 1.0);
    
    return output;
}
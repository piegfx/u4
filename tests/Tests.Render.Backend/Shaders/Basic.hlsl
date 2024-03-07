#include "Common.hlsli"

struct VSInput
{
    //uint Index: SV_VertexID;
    float3 Position: POSITION0;
    float4 Color: COLOR0;
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

    //output.Position = float4(RectVerts[RectIndices[input.Index]], 0.0, 1.0);
    //output.Color = RectColors[RectIndices[input.Index]];
    output.Position = float4(input.Position, 1.0);
    output.Color = input.Color;
    
    return output;
}

PSOutput Pixel(const in VSOutput input)
{
    PSOutput output;

    output.Color = input.Color;
    
    return output;
}
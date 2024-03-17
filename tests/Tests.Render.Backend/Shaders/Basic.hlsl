#include "Common.hlsli"

struct VSInput
{
    //uint Index: SV_VertexID;
    float3 Position: POSITION0;
    float2 TexCoord: TEXCOORD0;
    float4 Color:    COLOR0;
};

struct VSOutput
{
    float4 Position: SV_Position;
    float2 TexCoord: TEXCOORD0;
    float4 Color:    COLOR0;
};

struct PSOutput
{
    float4 Color: SV_Target0;
};

cbuffer TransformBuffer : register(b0)
{
    float4x4 TransformMatrix;
}

Texture2D Texture    : register(t1);
SamplerState Sampler : register(s1);

VSOutput Vertex(const in VSInput input)
{
    VSOutput output;

    //output.Position = float4(RectVerts[RectIndices[input.Index]], 0.0, 1.0);
    //output.Color = RectColors[RectIndices[input.Index]];
    output.Position = mul(TransformMatrix, float4(input.Position, 1.0));
    output.TexCoord = input.TexCoord;
    output.Color = input.Color;
    
    return output;
}

PSOutput Pixel(const in VSOutput input)
{
    PSOutput output;

    output.Color = Texture.Sample(Sampler, input.TexCoord) * input.Color;
    
    return output;
}
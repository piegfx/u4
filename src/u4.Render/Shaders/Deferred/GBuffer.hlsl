struct VSInput
{
    float3 Position: POSITION0;
    float2 TexCoord: TEXCOORD0;
    float3 Normal:   NORMAL0;
    float4 Color:    COLOR0;
};

struct VSOutput
{
    float4 Position: SV_Position;
    float2 TexCoord: TEXCOORD0;
    float3 Normal:   NORMAL0;
    float4 Color:    COLOR0;
};

struct PSOutput
{
    float4 Albedo: SV_Target0;
};

cbuffer CameraInfo : register(b0)
{
    float4x4 Projection;
    float4x4 View;
}

cbuffer DrawInfo : register(b1)
{
    float4x4 World;
}

Texture2D Albedo   : register(t2);
SamplerState State : register(s2);

VSOutput Vertex(const in VSInput input)
{
    VSOutput output;

    output.Position = mul(Projection, mul(View, mul(World, float4(input.Position, 1.0))));
    output.TexCoord = input.TexCoord;
    output.Normal = input.Normal;
    output.Color = input.Color;
    
    return output;
}

PSOutput Pixel(const in VSOutput input)
{
    PSOutput output;

    //output.Albedo = Albedo.Sample(State, input.TexCoord) * input.Color;
    output.Albedo = input.Color;
    
    return output;
}

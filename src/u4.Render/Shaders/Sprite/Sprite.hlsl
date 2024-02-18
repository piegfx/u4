struct VSInput
{
    float2 Position: POSITION0;
    float2 TexCoord: TEXCOORD0;
    float4 Tint:     COLOR0;
};

struct VSOutput
{
    float4 Position: SV_Position;
    float2 TexCoord: TEXCOORD0;
    float4 Tint:     COLOR0;
};

struct PSOutput
{
    float4 Color: SV_Target0;
};

cbuffer SpriteMatrdfgdfgices : register(b0)
{
    float4x4 ProjView;
}

Texture2D Sprite   : register(t1);
SamplerState State : register(s1);

VSOutput Vertex(const in VSInput input)
{
    VSOutput output;

    output.Position = mul(ProjView, float4(input.Position, 0.0, 1.0));
    output.TexCoord = input.TexCoord;
    output.Tint = input.Tint;
    
    return output;
}

PSOutput Pixel(const in VSOutput input)
{
    PSOutput output;

    output.Color = Sprite.Sample(State, input.TexCoord) * input.Tint;
    
    return output;
}
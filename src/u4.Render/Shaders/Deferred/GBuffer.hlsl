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



VSOutput Vertex(const in VSInput input)
{
    VSOutput output;

    
    
    return output;
}

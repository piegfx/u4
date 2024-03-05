static float2 RectVerts[] = {
    float2(-1.0, -1.0),
    float2(-1.0, +1.0),
    float2(+1.0, +1.0),
    float2(+1.0, -1.0)
};

static float4 RectColors[] = {
    float4(1.0, 0.0, 0.0, 1.0),
    float4(0.0, 1.0, 0.0, 1.0),
    float4(0.0, 0.0, 1.0, 1.0),
    float4(0.0, 0.0, 0.0, 1.0)
};

static uint RectIndices[] = {
    0, 1, 3,
    1, 2, 3
};
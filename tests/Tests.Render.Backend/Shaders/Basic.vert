#version 450

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec4 aColor;

out vec4 frag_Color;

const vec2 RectVerts[] = {
    vec2(-0.5, -0.5),
    vec2(-0.5, +0.5),
    vec2(+0.5, +0.5),
    vec2(+0.5, -0.5)
};

const vec4 RectColors[] = {
    vec4(1.0, 0.0, 0.0, 1.0),
    vec4(0.0, 1.0, 0.0, 1.0),
    vec4(0.0, 0.0, 1.0, 1.0),
    vec4(0.0, 0.0, 0.0, 1.0)
};

const uint RectIndices[] = {
    0, 1, 3,
    1, 2, 3
};

void main()
{
    //gl_Position = vec4(aPosition, 1.0);
    //frag_Color = aColor;
    
    gl_Position = RectVerts[RectIndices[gl_VertexID]];
    frag_Color = RectColors[RectIndices[gl_VertexID]];
}
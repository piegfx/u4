﻿using System.Numerics;
using u4.Math;
using u4.Render;
using u4.Render.Renderers;
using u4.Render.Structs;

namespace Tests.Render.Apps;

public class Basic3DTest : TestApp
{
    private Renderable _renderable;
    
    protected override void Initialize()
    {
        base.Initialize();

        Vertex[] vertices = new[]
        {
            new Vertex(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0), Vector3.Zero),
            new Vertex(new Vector3(-0.5f, +0.5f, 0), new Vector2(0, 1), Vector3.Zero),
            new Vertex(new Vector3(+0.5f, +0.5f, 0), new Vector2(1, 1), Vector3.Zero),
            new Vertex(new Vector3(+0.5f, -0.5f, 0), new Vector2(1, 0), Vector3.Zero)
        };

        uint[] indices = new uint[]
        {
            0, 1, 3,
            1, 2, 3
        };

        _renderable = new Renderable(vertices, indices);
    }

    protected override void Draw()
    {
        base.Draw();

        Renderer renderer = Graphics.Renderer;
        renderer.ClearColor = Color.CornflowerBlue;
        
        renderer.Clear();
        renderer.Draw(_renderable, Matrix4x4.Identity);

        System.Drawing.Size winSize = Window.FramebufferSize;
        Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(70 * (float.Pi / 180),
            winSize.Width / (float) winSize.Height, 0.1f, 100f);

        Matrix4x4 view = Matrix4x4.CreateLookAt(new Vector3(0, 0, 3), Vector3.Zero, Vector3.UnitY);
        
        renderer.Render3D(new CameraInfo(projection, view));
    }
}
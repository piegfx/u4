using System.Numerics;
using u4.Math;
using u4.Render;
using u4.Render.Renderers;
using u4.Render.Structs;

namespace Tests.Render.Apps;

public class Basic3DTest : TestApp
{
    private Renderable _renderable;
    private float _value;
    
    protected override void Initialize()
    {
        base.Initialize();

        Vertex[] vertices = new[]
        {
            new Vertex(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0), Vector3.Zero, Color.Red),
            new Vertex(new Vector3(-0.5f, +0.5f, 0), new Vector2(0, 1), Vector3.Zero, Color.Green),
            new Vertex(new Vector3(+0.5f, +0.5f, 0), new Vector2(1, 1), Vector3.Zero, Color.Blue),
            new Vertex(new Vector3(+0.5f, -0.5f, 0), new Vector2(1, 0), Vector3.Zero, Color.Yellow)
        };

        uint[] indices = new uint[]
        {
            0, 1, 3,
            1, 2, 3
        };

        _renderable = new Renderable(vertices, indices);
    }

    protected override void Update(float dt)
    {
        base.Update(dt);

        _value += dt;
    }

    protected override void Draw()
    {
        base.Draw();
        
        Graphics.Device.ClearColorBuffer(Color.Black);

        Renderer renderer = Graphics.Renderer;
        renderer.ClearColor = Color.CornflowerBlue;
        
        renderer.Clear();
        renderer.Draw(_renderable, Matrix4x4.CreateFromYawPitchRoll(_value, _value, _value));

        System.Drawing.Size winSize = Window.FramebufferSize;
        Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(70 * (float.Pi / 180),
            winSize.Width / (float) winSize.Height, 0.1f, 100f);

        Matrix4x4 view = Matrix4x4.CreateLookAt(new Vector3(0, 0, 3), Vector3.Zero, Vector3.UnitY);
        
        renderer.Perform3DPass(new CameraInfo(projection, view));
        
        renderer.Render();
    }
}
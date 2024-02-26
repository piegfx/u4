using System.Numerics;
using Pie.Windowing;
using u4.Engine;
using u4.Math;
using u4.Render;
using u4.Render.Renderers;

namespace Tests.Engine;

public class TestGame : Game
{
    private Texture _texture;
    private Vector2 _position;
    private float _scale;
    
    public override void Initialize()
    {
        base.Initialize();

        _texture = new Texture(@"C:\Users\ollie\Pictures\BAGELMIP.png");
        _scale = 1;
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        const float speed = 100;
        
        if (Input.KeyDown(Key.Right))
            _position.X += speed * dt;
        if (Input.KeyDown(Key.Left))
            _position.X -= speed * dt;
        if (Input.KeyDown(Key.Up))
            _position.Y -= speed * dt;
        if (Input.KeyDown(Key.Down))
            _position.Y += speed * dt;

        if (Input.MouseButtonPressed(MouseButton.Left))
            _scale += 0.1f;
        if (Input.MouseButtonPressed(MouseButton.Right))
            _scale -= 0.1f;
    }

    public override void Draw()
    {
        base.Draw();
        
        Graphics.Device.ClearColorBuffer(Color.CornflowerBlue);

        ref SpriteRenderer renderer = ref Graphics.SpriteRenderer;

        Vector2 texSize = (Vector2) _texture.Size;
        
        renderer.Begin();
        renderer.Draw(_texture, _position, Color.White, 0, new Vector2(_scale), texSize / 2);
        renderer.End();
    }
}
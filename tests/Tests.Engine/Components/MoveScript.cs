using Pie.Windowing;
using u4.Engine;
using u4.Engine.Entities;

namespace Tests.Engine.Components;

public class MoveScript : Component
{
    public float Speed;

    public MoveScript(float speed)
    {
        Speed = speed;
    }
    
    public override void Update(float dt)
    {
        base.Update(dt);

        if (Input.KeyDown(Key.W))
            Transform.Position.Y -= Speed * dt;
        if (Input.KeyDown(Key.S))
            Transform.Position.Y += Speed * dt;
        if (Input.KeyDown(Key.D))
            Transform.Position.X += Speed * dt;
        if (Input.KeyDown(Key.A))
            Transform.Position.X -= Speed * dt;
    }
}
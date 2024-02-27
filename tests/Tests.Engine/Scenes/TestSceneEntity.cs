using Pie.Windowing;
using Tests.Engine.Components;
using u4.Engine;
using u4.Engine.Entities;
using u4.Engine.Scenes;
using u4.Render;

namespace Tests.Engine.Scenes;

public class TestSceneEntity : Scene
{
    public override void Initialize()
    {
        base.Initialize();

        Entity entity = new Entity("Test");
        entity.AddComponent(new BasicSprite(new Texture(@"C:\Users\ollie\Pictures\awesomeface.png")));
        entity.AddComponent(new MoveScript(100));
        AddEntity(entity);
        
        //AddEntity(entity);
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        if (TryGetEntity("Test", out Entity entity))
            entity.GetComponent<BasicSprite>().Rotation += 1 * dt;
        
        if (Input.KeyPressed(Key.P))
            RemoveEntity("Test");
    }

    public override void Draw()
    {
        Graphics.SpriteRenderer.Begin();
        base.Draw();
        Graphics.SpriteRenderer.End();
    }
}
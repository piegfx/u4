using Tests.Engine.Components;
using u4.Engine.Entities;
using u4.Engine.Scenes;
using u4.Render;

namespace Tests.Engine.Scenes;

public class TestBasicEntity : Scene
{
    private Entity _entity;
    
    public override void Initialize()
    {
        base.Initialize();

        _entity = new Entity("Test");
        _entity.AddComponent(new BasicSprite(new Texture(@"C:\Users\ollie\Pictures\DEBUG.png")));
        _entity.AddComponent(new MoveScript(100));
        _entity.Initialize();
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        
        _entity.Update(dt);
    }

    public override void Draw()
    {
        base.Draw();
        
        Graphics.SpriteRenderer.Begin();
        _entity.Draw();
        Graphics.SpriteRenderer.End();
    }
}
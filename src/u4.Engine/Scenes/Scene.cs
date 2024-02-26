namespace u4.Engine.Scenes;

public abstract class Scene
{
    public virtual void Initialize() { }

    public virtual void Update(float dt) { }

    public virtual void Draw() { }

    public virtual void Unload() { }
}
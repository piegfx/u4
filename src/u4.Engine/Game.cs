using System;
using u4.Engine.Scenes;

namespace u4.Engine;

public class Game : IDisposable
{
    public virtual void Initialize()
    {
        SceneManager.Initialize();
    }

    public virtual void Update(float dt)
    {
        SceneManager.Update(dt);
    }

    public virtual void Draw()
    {
        SceneManager.Draw();
    }

    public virtual void Dispose() { }
}
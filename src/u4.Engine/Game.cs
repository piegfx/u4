using System;

namespace u4.Engine;

public class Game : IDisposable
{
    public virtual void Initialize() { }

    public virtual void Update(float dt) { }

    public virtual void Draw() { }

    public virtual void Dispose()
    {
        
    }
}
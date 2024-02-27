﻿namespace u4.Engine.Entities;

public abstract class Component
{
    public Entity Entity { get; internal set; }

    public ref Transform Transform => ref Entity.Transform;
    
    public virtual void Initialize() { }

    public virtual void Update(float dt) { }

    public virtual void Draw() { }

    public virtual void Unload() { }
}
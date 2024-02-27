using System;
using System.Collections.Generic;
using u4.Engine.Entities;

namespace u4.Engine.Scenes;

public abstract class Scene
{
    // TODO: Again, this is slow and inefficient. This should be replaced with an array + link.
    private Dictionary<string, Entity> _entities;
    private bool _isInitialized;

    protected Scene()
    {
        _entities = new Dictionary<string, Entity>();
    }

    public bool TryAddEntity(Entity entity)
    {
        if (!_entities.TryAdd(entity.Name, entity))
            return false;
        
        if (_isInitialized)
            entity.Initialize();

        return true;
    }

    public void AddEntity(Entity entity)
    {
        if (!TryAddEntity(entity))
            throw new Exception($"An entity with name \"{entity.Name}\" has already been added to the scene.");
    }

    public bool TryRemoveEntity(string name)
    {
        if (!_entities.Remove(name, out Entity entity))
            return false;
        
        entity.Unload();

        return true;
    }

    public void RemoveEntity(string name)
    {
        if (!TryRemoveEntity(name))
            throw new Exception($"No entity with name \"{name}\" has been added to the scene.");
    }

    public bool TryGetEntity(string name, out Entity entity)
    {
        return _entities.TryGetValue(name, out entity);
    }

    public Entity GetEntity(string name)
    {
        if (!TryGetEntity(name, out Entity entity))
            throw new Exception($"No entity with name \"{name}\" has been added to the scene.");

        return entity;
    }

    public virtual void Initialize()
    {
        if (_isInitialized)
            return;

        _isInitialized = true;
        
        foreach ((_, Entity entity) in _entities)
            entity.Initialize();
    }

    public virtual void Update(float dt)
    {
        foreach ((_, Entity entity) in _entities)
            entity.Update(dt);
    }

    public virtual void Draw()
    {
        foreach ((_, Entity entity) in _entities)
            entity.Draw();
    }

    public virtual void Unload()
    {
        foreach ((_, Entity entity) in _entities)
            entity.Unload();
    }
}
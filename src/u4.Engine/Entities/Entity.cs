using System;
using System.Collections.Generic;

namespace u4.Engine.Entities;

public class Entity
{
    // TODO: This is slow, this is just to get something basic working. Replace this with an array + link.
    private Dictionary<Type, Component> _components;
    private Transform _transform;

    private bool _isInitialized;
    
    public readonly string Name;

    public ref Transform Transform => ref _transform;

    public Entity(string name) : this(name, new Transform()) { }

    public Entity(string name, Transform transform)
    {
        Name = name;
        Transform = transform;

        _components = new Dictionary<Type, Component>();
    }

    public bool TryAddComponent<T>(T component) where T : Component
    {
        Type compType = typeof(T);

        if (!_components.TryAdd(compType, component))
            return false;

        component.Entity = this;
        
        if (_isInitialized)
            component.Initialize();

        return true;
    }

    public void AddComponent<T>(T component) where T : Component
    {
        if (!TryAddComponent(component))
            throw new Exception($"Component with type {typeof(T)} has already been added to the entity.");
    }

    public bool TryRemoveComponent<T>() where T : Component
    {
        Type compType = typeof(T);

        if (!_components.Remove(compType, out Component component))
            return false;
        
        component.Unload();

        return true;
    }

    public void RemoveComponent<T>() where T : Component
    {
        if (!TryRemoveComponent<T>())
            throw new Exception($"Could not remove component with type {typeof(T)}.");
    }

    public bool TryGetComponent<T>(out T component) where T : Component
    {
        component = null;
        
        if (!_components.TryGetValue(typeof(T), out Component comp))
            return false;

        component = (T) comp;
        
        return true;
    }

    public T GetComponent<T>() where T : Component
    {
        if (!TryGetComponent(out T component))
            throw new Exception($"Could not get component with type {typeof(T)}.");

        return component;
    }

    public void Initialize()
    {
        if (_isInitialized)
            return;

        _isInitialized = true;
        
        foreach ((_, Component component) in _components)
            component.Initialize();
    }

    public void Update(float dt)
    {
        foreach ((_, Component component) in _components)
            component.Update(dt);
    }

    public void Draw()
    {
        foreach ((_, Component component) in _components)
            component.Draw();
    }

    public void Unload()
    {
        foreach ((_, Component component) in _components)
            component.Unload();
    }
}
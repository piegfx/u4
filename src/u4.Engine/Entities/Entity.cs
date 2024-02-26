namespace u4.Engine.Entities;

public class Entity
{
    private Transform _transform;
    
    public readonly string Name;

    public ref Transform Transform => ref _transform;

    public Entity(string name)
    {
        Name = name;
        Transform = new Transform();
    }

    public Entity(string name, Transform transform)
    {
        Name = name;
        Transform = transform;
    }
    
    
}
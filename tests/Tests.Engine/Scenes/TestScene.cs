using System;
using u4.Engine.Scenes;

namespace Tests.Engine.Scenes;

public class TestScene : Scene
{
    public override void Initialize()
    {
        base.Initialize();
        
        Console.WriteLine("Initialize!");
    }
}
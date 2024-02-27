using System;
using u4.Core;

namespace u4.Engine.Scenes;

public static class SceneManager
{
    private static Scene _sceneToSwitch;
    
    internal static Scene ActiveScene;

    public static Scene CurrentScene => ActiveScene;

    public static void SetScene(Scene scene)
    {
        _sceneToSwitch = scene;
    }

    internal static void Initialize()
    {
        ActiveScene.Initialize();
    }

    internal static void Update(float dt)
    {
        if (_sceneToSwitch != null)
        {
            Logger.Debug("Scene change requested! Changing scenes.");
            
            ActiveScene.Unload();
            ActiveScene = null;
            GC.Collect();
            ActiveScene = _sceneToSwitch;
            _sceneToSwitch = null;
            ActiveScene.Initialize();
            
            Logger.Debug("Change completed.");
        }
        
        ActiveScene.Update(dt);
    }

    internal static void Draw()
    {
        ActiveScene.Draw();
    }
}
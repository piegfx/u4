using System.Collections.Generic;
using Pie.Windowing;

namespace u4.Engine;

public static class Input
{
    private static HashSet<Key> _keysDown;
    private static HashSet<Key> _newKeysDown;

    private static HashSet<MouseButton> _buttonsDown;
    private static HashSet<MouseButton> _newButtonsDown;
    
    internal static void Initialize()
    {
        _keysDown = new HashSet<Key>();
        _newKeysDown = new HashSet<Key>();

        _buttonsDown = new HashSet<MouseButton>();
        _newButtonsDown = new HashSet<MouseButton>();
        
        Window.KeyDown += WindowOnKeyDown;
        Window.KeyUp += WindowOnKeyUp;
        Window.MouseButtonDown += WindowOnMouseButtonDown;
        Window.MouseButtonUp += WindowOnMouseButtonUp;
    }

    internal static void Update()
    {
        _newKeysDown.Clear();
        _newButtonsDown.Clear();
    }

    private static void WindowOnKeyDown(Key key)
    {
        _keysDown.Add(key);
        _newKeysDown.Add(key);
    }

    private static void WindowOnKeyUp(Key key)
    {
        _keysDown.Remove(key);
        _newKeysDown.Remove(key);
    }

    private static void WindowOnMouseButtonDown(MouseButton button)
    {
        _buttonsDown.Add(button);
        _newButtonsDown.Add(button);
    }

    private static void WindowOnMouseButtonUp(MouseButton button)
    {
        _buttonsDown.Remove(button);
        _newButtonsDown.Remove(button);
    }
}
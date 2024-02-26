using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pie.Windowing;

namespace u4.Engine;

public static class Input
{
    private static HashSet<Key> _keysDown;
    private static HashSet<Key> _newKeysDown;

    private static HashSet<MouseButton> _buttonsDown;
    private static HashSet<MouseButton> _newButtonsDown;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool KeyDown(Key key)
        => _keysDown.Contains(key);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool KeyPressed(Key key)
        => _newKeysDown.Contains(key);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MouseButtonDown(MouseButton button)
        => _buttonsDown.Contains(button);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MouseButtonPressed(MouseButton button)
        => _newButtonsDown.Contains(button);
    
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
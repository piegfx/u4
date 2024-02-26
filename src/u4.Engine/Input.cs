using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Pie.Windowing;

namespace u4.Engine;

public static class Input
{
    private static HashSet<Key> _keysDown;
    private static HashSet<Key> _newKeysDown;

    private static HashSet<MouseButton> _buttonsDown;
    private static HashSet<MouseButton> _newButtonsDown;

    private static Vector2 _mousePosition;
    private static Vector2 _mouseDelta;
    private static Vector2 _scrollDelta;

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

    public static Vector2 MousePosition => _mousePosition;

    public static Vector2 MouseDelta => _mouseDelta;

    public static Vector2 ScrollDelta => _scrollDelta;
    
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
        
        Window.MouseMove += WindowOnMouseMove;
        Window.ScrollWheel += WindowOnScrollWheel;
    }

    internal static void Update()
    {
        _newKeysDown.Clear();
        _newButtonsDown.Clear();
        
        _mouseDelta = Vector2.Zero;
        _scrollDelta = Vector2.Zero;
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
    
    private static void WindowOnMouseMove(Vector2 position, Vector2 delta)
    {
        _mousePosition = position;
        _mouseDelta += delta;
    }
    
    private static void WindowOnScrollWheel(Vector2 delta)
    {
        _scrollDelta += delta;
    }
}
using System;
using System.Drawing;
using System.Numerics;
using Pie.Windowing;
using Pie.Windowing.Events;
using u4.Math;

namespace u4.Engine;

public static class Window
{
    private static string _userTitle;
    private static string _engineTitle;
    
    internal static Pie.Windowing.Window PieWindow;

    public static event OnCloseRequested CloseRequested = delegate { };

    public static event OnResized Resized = delegate { };

    public static event OnKeyDown KeyDown = delegate { };

    public static event OnKeyUp KeyUp = delegate { };

    public static event OnKeyRepeat KeyRepeat = delegate { };

    public static event OnMouseButtonDown MouseButtonDown = delegate { };

    public static event OnMouseButtonUp MouseButtonUp = delegate { };

    public static event OnMouseMove MouseMove = delegate { };

    public static event OnScrollWheel ScrollWheel = delegate { };

    public static event OnTextInput TextInput = delegate { };

    internal static string EngineTitle
    {
        get => _engineTitle;
        set
        {
            _engineTitle = value;
            PieWindow.Title = _userTitle + _engineTitle;
        }
    }

    public static Size<int> Size
    {
        get => (Size<int>) PieWindow.Size;
        set => PieWindow.Size = (System.Drawing.Size) value;
    }

    public static Size<int> FramebufferSize => (Size<int>) PieWindow.FramebufferSize;
    
    // TODO: Vector2<int>
    public static Point Position
    {
        get => PieWindow.Position;
        set => PieWindow.Position = value;
    }

    public static string Title
    {
        get => _userTitle;
        set
        {
            _userTitle = value;
            PieWindow.Title = _userTitle + _engineTitle;
        }
    }

    public static FullscreenMode FullscreenMode
    {
        get => PieWindow.FullscreenMode;
        set => PieWindow.FullscreenMode = value;
    }

    public static CursorMode CursorMode
    {
        get => PieWindow.CursorMode;
        set => PieWindow.CursorMode = value;
    }

    public static bool Resizable
    {
        get => PieWindow.Resizable;
        set => PieWindow.Resizable = value;
    }

    public static bool Borderless
    {
        get => PieWindow.Borderless;
        set => PieWindow.Borderless = value;
    }

    public static bool Visible
    {
        get => PieWindow.Visible;
        set => PieWindow.Visible = value;
    }

    public static bool Focused => PieWindow.Focused;

    public static void Focus() => PieWindow.Focus();

    public static void Center() => PieWindow.Center();

    public static void Maximize() => PieWindow.Maximize();

    public static void Minimize() => PieWindow.Minimize();

    public static void Restore() => PieWindow.Restore();

    internal static void Initialize(Pie.Windowing.Window window)
    {
        PieWindow = window;

        _userTitle = window.Title;
    }
    
    internal static void ProcessEvents()
    {
        while (PieWindow.PollEvent(out IWindowEvent winEvent))
        {
            switch (winEvent)
            {
                case QuitEvent:
                    CloseRequested.Invoke();
                    break;
                
                case ResizeEvent resize:
                    Resized.Invoke(new Size<int>(resize.Width, resize.Height));
                    break;
                
                case KeyEvent key:
                    switch (key.EventType)
                    {
                        case WindowEventType.KeyDown:
                            KeyDown.Invoke(key.Key);
                            break;
                        
                        case WindowEventType.KeyUp:
                            KeyUp.Invoke(key.Key);
                            break;
                        
                        case WindowEventType.KeyRepeat:
                            KeyRepeat.Invoke(key.Key);
                            break;
                        
                        default:
                            throw new ArgumentOutOfRangeException(nameof(key.EventType));
                    }

                    break;
                
                case MouseButtonEvent button:
                    switch (button.EventType)
                    {
                        case WindowEventType.MouseButtonDown:
                            MouseButtonDown.Invoke(button.Button);
                            break;
                        
                        case WindowEventType.MouseButtonUp:
                            MouseButtonUp.Invoke(button.Button);
                            break;
                        
                        default:
                            throw new ArgumentOutOfRangeException(nameof(button.EventType));
                    }

                    break;
                
                case MouseMoveEvent move:
                    MouseMove.Invoke(new Vector2(move.MouseX, move.MouseY), new Vector2(move.DeltaX, move.DeltaY));
                    break;
                
                case MouseScrollEvent scroll:
                    ScrollWheel.Invoke(new Vector2(scroll.X, scroll.Y));
                    break;
                
                case TextInputEvent textInput:
                    foreach (char c in textInput.Text)
                        TextInput.Invoke(c);
                    break;
            }
        }
    }

    public delegate void OnCloseRequested();

    public delegate void OnResized(Size<int> newSize);

    public delegate void OnKeyDown(Key key);

    public delegate void OnKeyUp(Key key);

    public delegate void OnKeyRepeat(Key key);

    public delegate void OnMouseButtonDown(MouseButton button);

    public delegate void OnMouseButtonUp(MouseButton button);

    public delegate void OnMouseMove(Vector2 position, Vector2 delta);

    public delegate void OnScrollWheel(Vector2 delta);

    public delegate void OnTextInput(char c);
}
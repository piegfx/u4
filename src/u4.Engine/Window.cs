using System;
using System.Drawing;
using Pie.Windowing;
using Pie.Windowing.Events;
using u4.Math;

namespace u4.Engine;

public static class Window
{
    private static string _userTitle;
    private static string _engineTitle;
    
    internal static Pie.Windowing.Window PieWindow;

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

    internal static void ProcessEvents()
    {
        while (PieWindow.PollEvent(out IWindowEvent winEvent))
        {
            switch (winEvent)
            {
                case QuitEvent:
                    Console.WriteLine("asdasda");
                    break;
            }
        }
    }
}
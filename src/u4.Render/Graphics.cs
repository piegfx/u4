using System;
using System.Collections.Concurrent;
using Pie;
using u4.Core;
using u4.Math;
using u4.Render.Renderers;

namespace u4.Render;

public static class Graphics
{
    private static bool _vsync;
    private static ConcurrentBag<Action> _actions;
    
    public static GraphicsDevice Device;

    public static SpriteRenderer SpriteRenderer;

    public static Renderer Renderer;

    public static bool VSync
    {
        get => _vsync;
        set => _vsync = value;
    }

    public static void Initialize(GraphicsDevice device, Size<int> size)
    {
        Device = device;

        _actions = new ConcurrentBag<Action>();

        Logger.Trace("Creating sprite renderer.");
        SpriteRenderer = new SpriteRenderer(device);
        
        Logger.Trace("Creating main renderer.");
        Renderer = new DeferredRenderer(device, size);

        VSync = true;
    }

    public static void Deinitialize()
    {
        SpriteRenderer.Dispose();
        Device.Dispose();
    }

    public static void Resize(in Size<int> size)
    {
        Device.ResizeSwapchain((System.Drawing.Size) size);
    }

    public static void Present()
    {
        Device.Present(_vsync ? 1 : 0);
        
        foreach (Action action in _actions)
            action.Invoke();
        
        _actions.Clear();
    }

    public static void RunOnGraphicsThread(Action action)
    {
        _actions.Add(action);
    }
}

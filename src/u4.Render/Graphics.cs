using Pie;
using u4.Core;
using u4.Math;
using u4.Render.Renderers;

namespace u4.Render;

public static class Graphics
{
    public static GraphicsDevice Device;

    public static SpriteRenderer SpriteRenderer;

    public static Renderer Renderer;

    public static void Initialize(GraphicsDevice device, Size<int> size)
    {
        Device = device;

        Logger.Trace("Creating sprite renderer.");
        SpriteRenderer = new SpriteRenderer(device);
        
        Logger.Trace("Creating main renderer.");
        Renderer = new DeferredRenderer(device, size);
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
        Device.Present(1);
    }
}

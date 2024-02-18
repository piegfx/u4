using Pie;
using u4.Core;
using u4.Render.Renderers;

namespace u4.Render;

public static class Graphics
{
    public static GraphicsDevice Device;

    public static SpriteRenderer SpriteRenderer;

    public static void Initialize(GraphicsDevice device)
    {
        Device = device;

        Logger.Trace("Creating sprite renderer.");
        SpriteRenderer = new SpriteRenderer(device);
    }

    public static void Deinitialize()
    {
        SpriteRenderer.Dispose();
        Device.Dispose();
    }

    public static void Present()
    {
        Device.Present(1);
    }
}

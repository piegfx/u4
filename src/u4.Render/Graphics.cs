using Pie;

namespace u4.Render;

public static class Graphics
{
    public static GraphicsDevice Device;

    public static void Initialize(GraphicsDevice device)
    {
        Device = device;
    }

    public static void Deinitialize()
    {
        Device.Dispose();
    }

    public static void Present()
    {
        Device.Present(1);
    }
}

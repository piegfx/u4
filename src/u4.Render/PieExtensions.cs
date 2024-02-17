using Pie;
using u4.Math;

namespace u4.Render;

public static class PieExtensions
{
    public static void ClearColorBuffer(this GraphicsDevice device, in Color color)
    {
        device.ClearColorBuffer(color.R, color.G, color.B, color.A);
    }
}
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D_PRIMITIVE_TOPOLOGY;

namespace u4.Render.Backend.D3D11;

internal static class D3D11Utils
{
    public static D3D_PRIMITIVE_TOPOLOGY ToPrimitiveTopology(this PrimitiveType type)
    {
        return type switch
        {
            PrimitiveType.TriangleList => D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
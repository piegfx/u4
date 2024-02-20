using System.Numerics;

namespace u4.Render.Structs;

public struct CameraInfo
{
    public Matrix4x4 Projection;

    public Matrix4x4 View;

    public CameraInfo(Matrix4x4 projection, Matrix4x4 view)
    {
        Projection = projection;
        View = view;
    }
}
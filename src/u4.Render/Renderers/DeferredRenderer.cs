using System.Collections.Generic;
using System.Numerics;
using Pie;
using u4.Math;
using u4.Render.Structs;

namespace u4.Render.Renderers;

public class DeferredRenderer : Renderer
{
    private readonly GraphicsDevice _device;
    
    private readonly List<TransformedRenderable> _opaques;

    private readonly Framebuffer _gBuffer;
    private readonly Pie.Texture _albedoBuffer;
    private readonly Pie.Texture _depthBuffer;
    
    public DeferredRenderer(GraphicsDevice device, Size<int> size) : base(size)
    {
        _device = device;
        
        _opaques = new List<TransformedRenderable>();

        TextureDescription gBufferDesc = TextureDescription.Texture2D(size.Width, size.Height,
            Format.R32G32B32A32_Float, 1, 1, TextureUsage.Framebuffer | TextureUsage.ShaderResource);

        _albedoBuffer = device.CreateTexture(gBufferDesc);

        gBufferDesc.Format = Format.D32_Float;
        gBufferDesc.Usage = TextureUsage.Framebuffer;

        _depthBuffer = device.CreateTexture(gBufferDesc);

        _gBuffer = device.CreateFramebuffer(new[]
        {
            new FramebufferAttachment(_albedoBuffer),
            new FramebufferAttachment(_depthBuffer)
        });
    }

    public override void Clear()
    {
        _opaques.Clear();
    }

    public override void Draw(Renderable renderable, Matrix4x4 world)
    {
        _opaques.Add(new TransformedRenderable(renderable, world));
    }

    public override void Render3D(in CameraInfo camera)
    {
        _device.SetFramebuffer(_gBuffer);
        
        
    }

    public override void Dispose()
    {
        _gBuffer.Dispose();
        _albedoBuffer.Dispose();
        _depthBuffer.Dispose();
    }
}
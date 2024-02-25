using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using Pie;
using Pie.ShaderCompiler;
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

    private Shader _gBufferShader;
    private InputLayout _gBufferInputLayout;

    private readonly GraphicsBuffer _cameraBuffer;
    private readonly GraphicsBuffer _drawInfoBuffer;
    
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

        byte[] vertSpv = File.ReadAllBytes("Content/Shaders/Deferred/GBuffer_vert.spv");
        byte[] fragSpv = File.ReadAllBytes("Content/Shaders/Deferred/GBuffer_frag.spv");

        _gBufferShader = device.CreateShader(new[]
        {
            new ShaderAttachment(ShaderStage.Vertex, vertSpv, "Vertex"),
            new ShaderAttachment(ShaderStage.Fragment, fragSpv, "Pixel")
        });

        _gBufferInputLayout = device.CreateInputLayout(new[]
        {
            new InputLayoutDescription(Format.R32G32B32_Float, 0, 0, InputType.PerVertex), // Position
            new InputLayoutDescription(Format.R32G32_Float, 12, 0, InputType.PerVertex), // TexCoord
            new InputLayoutDescription(Format.R32G32B32_Float, 20, 0, InputType.PerVertex), // Normal
            new InputLayoutDescription(Format.R32G32B32A32_Float, 32, 0, InputType.PerVertex) // Color
        });

        _cameraBuffer = device.CreateBuffer(BufferType.UniformBuffer, Unsafe.SizeOf<CameraInfo>(), true);
        _drawInfoBuffer = device.CreateBuffer(BufferType.UniformBuffer, Unsafe.SizeOf<DrawInfo>(), true);
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
        _device.ClearColorBuffer(0.0f, 0.0f, 0.0f, 0.0f);
        _device.ClearDepthStencilBuffer(ClearFlags.Depth, 0.0f, 1);
        
        
    }

    public override void Dispose()
    {
        _drawInfoBuffer.Dispose();
        _cameraBuffer.Dispose();
        
        _gBuffer.Dispose();
        _albedoBuffer.Dispose();
        _depthBuffer.Dispose();
    }
}
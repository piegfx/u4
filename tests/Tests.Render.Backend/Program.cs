using System;
using System.IO;
using Silk.NET.OpenGL;
using Silk.NET.SDL;
using StbImageSharp;
using u4.Math;
using u4.Render.Backend;
using u4.Render.Backend.D3D11;
using u4.Render.Backend.GL45;
using Color = u4.Math.Color;
using PrimitiveType = u4.Render.Backend.PrimitiveType;
using Shader = u4.Render.Backend.Shader;
using Texture = u4.Render.Backend.Texture;

unsafe
{
    Sdl sdl = Sdl.GetApi();
    
    if (sdl.Init(Sdl.InitVideo | Sdl.InitEvents) < 0)
        throw new Exception("Failed to initialize SDL.");

    Size<int> size = new Size<int>(1280, 720);

    sdl.GLSetAttribute(GLattr.ContextMajorVersion, 4);
    sdl.GLSetAttribute(GLattr.ContextMinorVersion, 5);
    sdl.GLSetAttribute(GLattr.ContextProfileMask, (int) ContextProfileMask.CoreProfileBit);
    sdl.GLSetAttribute(GLattr.DepthSize, 0);

    Window* window = sdl.CreateWindow("Test", Sdl.WindowposCentered, Sdl.WindowposCentered, size.Width, size.Height,
        (uint) (WindowFlags.Shown | WindowFlags.Resizable));

    if (window == null)
        throw new Exception("Failed to create window.");

    //void* sdlGlContext = sdl.GLCreateContext(window);
    //sdl.GLMakeCurrent(window, sdlGlContext);

    SysWMInfo info = new SysWMInfo();
    sdl.GetWindowWMInfo(window, &info);

    GraphicsDevice device = new D3D11GraphicsDevice(info.Info.Win.Hwnd, size.As<uint>());

    /*GraphicsDevice device = new GL45GraphicsDevice(new GLContext(s => (nint) sdl.GLGetProcAddress(s), i =>
    {
        sdl.GLSetSwapInterval(i);
        sdl.GLSwapWindow(window);
    }), size.As<uint>());*/
    
    sdl.SetWindowTitle(window, sdl.GetWindowTitleS(window) + $" - {device.Api}");

    ReadOnlySpan<float> vertices = stackalloc float[]
    {
        -0.5f, -0.5f, 0.0f,    0.0f, 1.0f,    1.0f, 0.0f, 0.0f, 1.0f,
        -0.5f, +0.5f, 0.0f,    0.0f, 0.0f,    0.0f, 1.0f, 0.0f, 1.0f,
        +0.5f, +0.5f, 0.0f,    1.0f, 0.0f,    0.0f, 0.0f, 1.0f, 1.0f,
        +0.5f, -0.5f, 0.0f,    1.0f, 1.0f,    1.0f, 1.0f, 0.0f, 1.0f
    };

    ReadOnlySpan<uint> indices = stackalloc uint[]
    {
        0, 1, 3,
        1, 2, 3
    };

    GraphicsBuffer vertexBuffer =
        device.CreateBuffer(new BufferDescription(BufferType.Vertex, (uint) vertices.Length * sizeof(float), false),
            vertices);

    GraphicsBuffer indexBuffer =
        device.CreateBuffer(new BufferDescription(BufferType.Index, (uint) indices.Length * sizeof(uint), false),
            indices);

    ShaderModule vertexShader = device.CreateShaderModuleFromFile("Shaders/Basic.hlsl", ShaderStage.Vertex, "Vertex");
    ShaderModule pixelShader = device.CreateShaderModuleFromFile("Shaders/Basic.hlsl", ShaderStage.Pixel, "Pixel");

    ShaderAttachment[] attachments = new[]
    {
        new ShaderAttachment(vertexShader, ShaderStage.Vertex),
        new ShaderAttachment(pixelShader, ShaderStage.Pixel)
    };

    Shader shader = device.CreateShader(attachments);

    InputLayout layout = device.CreateInputLayout(new InputLayoutDescription[]
    {
        new InputLayoutDescription("POSITION", 0, Format.R32G32B32Float, 0, 0, InputType.PerVertex),
        new InputLayoutDescription("TEXCOORD", 0, Format.R32G32Float, 12, 0, InputType.PerVertex),
        new InputLayoutDescription("COLOR", 0, Format.R32G32B32A32Float, 20, 0, InputType.PerVertex)
    }, vertexShader);
    
    pixelShader.Dispose();
    vertexShader.Dispose();
    
    ImageResult result = ImageResult.FromMemory(File.ReadAllBytes("Content/bagel.png"));

    Texture texture =
        device.CreateTexture<byte>(
            TextureDescription.Texture2D((uint) result.Width, (uint) result.Height, Format.R8G8B8A8UNorm, 0, 1,
                TextureUsage.ShaderResource | TextureUsage.GenerateMipmaps), result.Data);
    
    device.GenerateMipmaps(texture);

    bool shouldClose = false;
    while (!shouldClose)
    {
        Event sEvent;
        while (sdl.PollEvent(&sEvent) != 0)
        {
            switch ((EventType) sEvent.Type)
            {
                case EventType.Windowevent:
                    switch ((WindowEventID) sEvent.Window.Event)
                    {
                        case WindowEventID.Close:
                            shouldClose = true;
                            break;
                        
                        case WindowEventID.Resized:
                            device.ResizeSwapchain(new Size<int>(sEvent.Window.Data1, sEvent.Window.Data2));
                            device.Viewport = new Viewport(0, 0, (uint) sEvent.Window.Data1, (uint) sEvent.Window.Data2);
                            break;
                    }
                    break;
            }
        }
        
        device.ClearColorBuffer(Color.CornflowerBlue);
        
        device.SetShader(shader);
        device.SetPrimitiveType(PrimitiveType.TriangleList);
        
        device.SetTexture(0, texture);
        
        device.SetVertexBuffer(0, vertexBuffer, 9 * sizeof(float));
        device.SetIndexBuffer(indexBuffer, Format.R32UInt);
        device.SetInputLayout(layout);
        
        device.DrawIndexed(6);
        
        device.Present();
    }
    
    texture.Dispose();
    
    layout.Dispose();
    shader.Dispose();
    
    indexBuffer.Dispose();
    vertexBuffer.Dispose();
    
    device.Dispose();
    
    sdl.DestroyWindow(window);
    sdl.Quit();
    
    sdl.Dispose();
}
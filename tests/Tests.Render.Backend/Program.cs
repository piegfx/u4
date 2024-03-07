﻿using System;
using Silk.NET.SDL;
using u4.Math;
using u4.Render.Backend;
using u4.Render.Backend.D3D11;
using Color = u4.Math.Color;

unsafe
{
    Sdl sdl = Sdl.GetApi();
    
    if (sdl.Init(Sdl.InitVideo | Sdl.InitEvents) < 0)
        throw new Exception("Failed to initialize SDL.");

    Size<int> size = new Size<int>(1280, 720);

    Window* window = sdl.CreateWindow("Test", Sdl.WindowposCentered, Sdl.WindowposCentered, size.Width, size.Height,
        (uint) (WindowFlags.Shown | WindowFlags.Resizable));

    if (window == null)
        throw new Exception("Failed to create window.");

    SysWMInfo info = new SysWMInfo();
    sdl.GetWindowWMInfo(window, &info);

    GraphicsDevice device = new D3D11GraphicsDevice(info.Info.Win.Hwnd, size.As<uint>());

    ReadOnlySpan<float> vertices = stackalloc float[]
    {
        -0.5f, -0.5f, 0.0f,    1.0f, 0.0f, 0.0f, 1.0f,
        -0.5f, +0.5f, 0.0f,    0.0f, 1.0f, 0.0f, 1.0f,
        +0.5f, +0.5f, 0.0f,    0.0f, 0.0f, 1.0f, 1.0f,
        +0.5f, -0.5f, 0.0f,    0.0f, 0.0f, 0.0f, 1.0f
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
        new InputLayoutDescription("COLOR", 0, Format.R32G32B32A32Float, 12, 0, InputType.PerVertex)
    }, vertexShader);
    
    pixelShader.Dispose();
    vertexShader.Dispose();

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
        
        device.SetInputLayout(layout);
        device.SetVertexBuffer(0, vertexBuffer, 7 * sizeof(float));
        device.SetIndexBuffer(indexBuffer, Format.R32UInt);
        
        device.DrawIndexed(6);
        
        device.Present();
    }
    
    layout.Dispose();
    shader.Dispose();
    
    indexBuffer.Dispose();
    vertexBuffer.Dispose();
    
    device.Dispose();
    
    sdl.DestroyWindow(window);
    sdl.Quit();
    
    sdl.Dispose();
}
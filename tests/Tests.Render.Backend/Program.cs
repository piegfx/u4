using System;
using Pie.SDL;
using u4.Math;
using u4.Render.Backend;
using u4.Render.Backend.D3D11;

unsafe
{
    if (Sdl.Init(Sdl.InitVideo | Sdl.InitEvents) < 0)
        throw new Exception("Failed to initialize SDL.");

    Size<int> size = new Size<int>(1280, 720);
    
    void* window;
    fixed (byte* title = "Test"u8)
    {
        window = Sdl.CreateWindow((sbyte*) title, (int) Sdl.WindowposCentered, (int) Sdl.WindowposCentered, size.Width,
            size.Height, (uint) SdlWindowFlags.Shown);
    }

    if (window == null)
        throw new Exception("Failed to create window.");

    SdlSysWmInfo info = new SdlSysWmInfo();
    Sdl.GetWindowWMInfo(window, &info);

    GraphicsDevice device = new D3D11GraphicsDevice(info.Info.Win.Window, size.As<uint>());

    bool shouldClose = false;
    while (!shouldClose)
    {
        SdlEvent sEvent;
        while (Sdl.PollEvent(&sEvent))
        {
            switch ((SdlEventType) sEvent.Type)
            {
                case SdlEventType.WindowEvent:
                    switch ((SdlWindowEventId) sEvent.Window.Event)
                    {
                        case SdlWindowEventId.Close:
                            shouldClose = true;
                            break;
                    }
                    break;
            }
        }
        
        device.Present();
    }
    
    Sdl.DestroyWindow(window);
    Sdl.Quit();
}
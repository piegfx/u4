using System;
using System.Diagnostics;
using Pie;
using Pie.Windowing;
using Pie.Windowing.Events;
using u4.Math;
using u4.Render;

namespace Tests.Render;

public abstract class TestApp : IDisposable
{
    protected Window Window;
    
    protected virtual void Initialize() { }

    protected virtual void Update(float dt) { }

    protected virtual void Draw() { }

    public void Run()
    {
        Size<int> winSize = new Size<int>(1280, 720);
        
        Window = new WindowBuilder()
            .Size(winSize.Width, winSize.Height)
            .Title("Graphics Tests")
            .Resizable()
            .Build(out GraphicsDevice device);
        
        Graphics.Initialize(device, winSize);
        
        Initialize();
        
        Stopwatch sw = Stopwatch.StartNew();

        bool isAlive = true;
        while (isAlive)
        {
            while (Window.PollEvent(out IWindowEvent winEvent))
            {
                switch (winEvent)
                {
                    case QuitEvent:
                        isAlive = false;
                        break;
                    
                    case ResizeEvent newSize:
                        Graphics.Resize(new Size<int>(newSize.Width, newSize.Height));
                        break;
                }
            }

            float delta = (float) sw.Elapsed.TotalSeconds;
            sw.Restart();
            
            Update(delta);
            Draw();
            
            Graphics.Present();
        }
    }

    public virtual void Dispose()
    {
        Graphics.Deinitialize();
        Window.Dispose();
    }
}
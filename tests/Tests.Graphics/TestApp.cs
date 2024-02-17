using System;
using System.Diagnostics;
using Pie;
using Pie.Windowing;
using Pie.Windowing.Events;

namespace Tests.Graphics;

public abstract class TestApp : IDisposable
{
    protected Window Window;
    protected GraphicsDevice GraphicsDevice;
    
    protected virtual void Initialize() { }

    protected virtual void Update(float dt) { }

    protected virtual void Draw() { }

    public void Run()
    {
        Window = new WindowBuilder()
            .Size(1280, 720)
            .Title("Graphics Tests")
            .Build(out GraphicsDevice);
        
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
                }
            }

            float delta = (float) sw.Elapsed.TotalSeconds;
            sw.Restart();
            
            Update(delta);
            Draw();
            
            GraphicsDevice.Present(1);
        }
    }

    public virtual void Dispose()
    {
        GraphicsDevice.Dispose();
        Window.Dispose();
    }
}
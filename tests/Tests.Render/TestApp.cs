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
        Window = new WindowBuilder()
            .Size(1280, 720)
            .Title("Graphics Tests")
            .Resizable()
            .Build(out GraphicsDevice device);
        
        Graphics.Initialize(device);
        
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
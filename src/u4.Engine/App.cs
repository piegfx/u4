﻿using Pie;
using Pie.Windowing;
using u4.Core;
using u4.Render;

namespace u4.Engine;

public static class App
{
    private static bool _isAlive;
    
    public static string Name { get; private set; }
    
    public static string Version { get; private set; }

    public static Game Game;
    
    public static void Run(LaunchOptions options, Game game)
    {
        Name = options.AppName;
        Version = options.Version;
        
        Logger.Info($"{Name} {Version}, starting up.");

        Game = game;

        GraphicsDeviceOptions deviceOptions = new GraphicsDeviceOptions()
        {
            Debug = true,

            DepthStencilBufferFormat = null
        };
        
        Logger.Debug("Creating window.");
        WindowBuilder builder = new WindowBuilder()
            .Size(options.Size.Width, options.Size.Height)
            .Title(options.Title)
            .GraphicsDeviceOptions(deviceOptions);

        Window.Initialize(builder.Build());
        Window.CloseRequested += Quit;

        Logger.Debug("Initializing graphics subsystem.");
        GraphicsDevice device = Window.PieWindow.CreateGraphicsDevice(deviceOptions);
        Graphics.Initialize(device);
        
        Window.EngineTitle = $" - {device.Api}";
        
        Logger.Debug("Initializing user code.");
        Game.Initialize();

        _isAlive = true;

        while (_isAlive)
        {
            Window.ProcessEvents();
            
            Game.Update(1.0f);
            Game.Draw();
            
            Graphics.Present();
        }
        
        Logger.Trace("Disposing graphics.");
        Graphics.Deinitialize();
        
        Logger.Trace("Disposing window.");
        Window.PieWindow.Dispose();
    }

    public static void Quit()
    {
        _isAlive = false;
    }
}

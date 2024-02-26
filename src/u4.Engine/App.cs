using Pie;
using Pie.Windowing;
using u4.Core;
using u4.Engine.Scenes;
using u4.Render;

namespace u4.Engine;

public static class App
{
    private static bool _isAlive;
    
    public static string Name { get; private set; }
    
    public static string Version { get; private set; }

    public static Game Game;
    
    public static void Run(LaunchOptions options, Game game, Scene scene)
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
        Graphics.Initialize(device, options.Size);
        
        Window.EngineTitle = $" - {device.Api}";
        
        Logger.Debug("Initializing input subsystem.");
        Input.Initialize();
        
        Logger.Debug("Initializing time subsystem.");
        Time.Initialize();
        
        Logger.Debug("Initializing game.");
        SceneManager.ActiveScene = scene;
        // Remove gc reference.
        scene = null;
        Game.Initialize();

        Logger.Debug("Entering main loop.");
        
        _isAlive = true;
        while (_isAlive)
        {
            if (Time.Update())
                continue;
            
            Input.Update();
            Window.ProcessEvents();
            
            Game.Update(Time.DeltaTime);
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

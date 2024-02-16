using Pie;
using Pie.Windowing;
using u4.Core;

namespace u4.Engine;

public static class App
{
    private static bool _isAlive;
    
    public static string Name { get; private set; }
    
    public static string Version { get; private set; }
    
    public static void Run(LaunchOptions options)
    {
        Name = options.AppName;
        Version = options.Version;

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

        Window.PieWindow = builder.Build();

        GraphicsDevice device = Window.PieWindow.CreateGraphicsDevice(deviceOptions);

        _isAlive = true;

        while (_isAlive)
        {
            Window.ProcessEvents();
            
            device.Present(1);
        }
    }
}

using u4.Core;
using u4.Engine;
using u4.Math;

Logger.AttachConsole();

LaunchOptions options = new LaunchOptions()
{
    AppName = "Test",
    Version = "1.0.0",
    Size = new Size<int>(1280, 720),
    Title = "Test"
};

App.Run(options);
using System.Reflection;
using u4.Math;

namespace u4.Engine;

public struct LaunchOptions
{
    public string AppName;

    public string Version;

    public Size<int> Size;

    public string Title;

    public static LaunchOptions Default
    {
        get
        {
            string appName = Assembly.GetEntryAssembly()?.GetName().Name ?? "u4 Application";

            return new LaunchOptions()
            {
                AppName = appName,
                Version = "1.0.0",
                Size = new Size<int>(1280, 720),
                Title = appName
            };
        }
    }
}
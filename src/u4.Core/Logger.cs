using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
// ReSharper disable ExplicitCallerInfoArgument

namespace u4.Core;

public static class Logger
{
    private static StringBuilder _builder;
    
    public static event OnLogMessage LogMessage = delegate { };

    static Logger()
    {
        _builder = new StringBuilder();
    }
    
    public static void Log(LogType type, string message, [CallerLineNumber] int line = 0,
        [CallerFilePath] string path = "")
    {
        _builder.Clear();

        _builder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff "));
        
        _builder.Append(type switch
        {
            LogType.Trace   => "[Trace] ",
            LogType.Debug   => "[Debug] ",
            LogType.Info    => "[Info]  ",
            LogType.Warning => "[Warn]  ",
            LogType.Error   => "[Error] ",
            LogType.Fatal   => "[Fatal] ",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        });
        
        string fName = Path.GetFileName(path);
        _builder.Append($"{fName}:{line} ");

        _builder.Append(message);
        
        LogMessage.Invoke(type, _builder.ToString());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Trace(string message, [CallerLineNumber] int line = 0, [CallerFilePath] string path = "")
        => Log(LogType.Trace, message, line, path);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Debug(string message, [CallerLineNumber] int line = 0, [CallerFilePath] string path = "")
        => Log(LogType.Debug, message, line, path);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Info(string message, [CallerLineNumber] int line = 0, [CallerFilePath] string path = "")
        => Log(LogType.Info, message, line, path);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warn(string message, [CallerLineNumber] int line = 0, [CallerFilePath] string path = "")
        => Log(LogType.Warning, message, line, path);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error(string message, [CallerLineNumber] int line = 0, [CallerFilePath] string path = "")
        => Log(LogType.Error, message, line, path);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal(string message, [CallerLineNumber] int line = 0, [CallerFilePath] string path = "")
        => Log(LogType.Fatal, message, line, path);

    public static void AttachConsole()
    {
        LogMessage += (type, message) =>
        {
            ConsoleColor color = Console.ForegroundColor;

            switch (type)
            {
                case LogType.Trace:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogType.Debug:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogType.Fatal:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            
            Console.WriteLine(message);

            Console.ForegroundColor = color;
        };
    }
    
    public enum LogType
    {
        Trace,
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    public delegate void OnLogMessage(LogType type, string message);
}
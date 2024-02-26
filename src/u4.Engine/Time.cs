using System;
using System.Diagnostics;

namespace u4.Engine;

public static class Time
{
    private static Stopwatch _deltaWatch;
    private static double _deltaTime;

    private static Stopwatch _totalWatch;

    public static float DeltaTime => (float) _deltaTime;

    public static double DeltaTimeD => _deltaTime;

    public static TimeSpan TotalTime => _totalWatch.Elapsed;

    internal static void Initialize()
    {
        _deltaWatch = Stopwatch.StartNew();
        _totalWatch = Stopwatch.StartNew();
    }

    internal static bool Update()
    {
        _deltaTime = _deltaWatch.Elapsed.TotalSeconds;
        _deltaWatch.Restart();
        
        return false;
    }
}
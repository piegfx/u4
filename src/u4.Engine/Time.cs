using System;
using System.Diagnostics;

namespace u4.Engine;

public static class Time
{
    private static Stopwatch _deltaWatch;
    private static Stopwatch _totalWatch;
    
    private static double _deltaTime;
    private static double _targetDelta;
    private static int _targetFps;
    
    private static ulong _totalFrames;
    private static double _timeCount;
    private static int _frameCount;
    private static int _fps;

    public static float DeltaTime => (float) _deltaTime;

    public static double DeltaTimeD => _deltaTime;

    public static TimeSpan TotalTime => _totalWatch.Elapsed;

    public static int FPS => _fps;

    public static ulong TotalFrames => _totalFrames;

    public static int TargetFPS
    {
        get => _targetFps;
        set
        {
            _targetDelta = value == 0 ? double.Epsilon : 1.0 / value;
            _targetFps = value;
        }
    }

    internal static void Initialize()
    {
        _deltaWatch = Stopwatch.StartNew();
        _totalWatch = Stopwatch.StartNew();

        TargetFPS = 0;
    }

    internal static bool Update()
    {
        _deltaTime = _deltaWatch.Elapsed.TotalSeconds;
        if (_deltaTime < _targetDelta)
            return true;
        
        _deltaWatch.Restart();

        _timeCount += _deltaTime;
        _frameCount++;
        _totalFrames++;

        if (_timeCount >= 1.0)
        {
            _timeCount -= 1.0;
            _fps = _frameCount;
            _frameCount = 0;
        }
        
        return false;
    }
}
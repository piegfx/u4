namespace u4.Render.Backend.GL45;

public class GLContext
{
    public Func<string, nint> GetProcAddressFunc;

    public Action<int> PresentFunc;

    public GLContext(Func<string, IntPtr> getProcAddressFunc, Action<int> presentFunc)
    {
        GetProcAddressFunc = getProcAddressFunc;
        PresentFunc = presentFunc;
    }

    public nint GetProcAddress(string procName) => GetProcAddressFunc.Invoke(procName);

    public void Present(int interval) => PresentFunc.Invoke(interval);
}
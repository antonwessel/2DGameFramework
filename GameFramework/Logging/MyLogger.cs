using System.Diagnostics;

namespace GameFramework.Logging;

public sealed class MyLogger
{
    public static MyLogger Instance { get; } = new MyLogger();

    private MyLogger()
    {

    }

    public void Log(string message)
    {
        Trace.WriteLine(message);
    }

    public void AddListener(TraceListener listener)
    {
        ArgumentNullException.ThrowIfNull(listener);
        Trace.Listeners.Add(listener);
    }

    public void RemoveListener(TraceListener listener)
    {
        ArgumentNullException.ThrowIfNull(listener);
        Trace.Listeners.Remove(listener);
    }
}

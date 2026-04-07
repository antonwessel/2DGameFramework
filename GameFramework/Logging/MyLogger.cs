using System.Diagnostics;

namespace GameFramework.Logging;

/// <summary>
/// Simple shared logger for trace output.
/// </summary>
public sealed class MyLogger
{
    /// <summary>
    /// Gets the shared logger instance.
    /// </summary>
    public static MyLogger Instance { get; } = new MyLogger();

    private MyLogger()
    {

    }

    /// <summary>
    /// Writes a message to the trace output.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public void Log(string message)
    {
        Trace.WriteLine(message);
    }

    /// <summary>
    /// Adds a trace listener.
    /// </summary>
    /// <param name="listener">The listener to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="listener"/> is null.</exception>
    public void AddListener(TraceListener listener)
    {
        ArgumentNullException.ThrowIfNull(listener);
        Trace.Listeners.Add(listener);
    }

    /// <summary>
    /// Removes a trace listener.
    /// </summary>
    /// <param name="listener">The listener to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="listener"/> is null.</exception>
    public void RemoveListener(TraceListener listener)
    {
        ArgumentNullException.ThrowIfNull(listener);
        Trace.Listeners.Remove(listener);
    }
}

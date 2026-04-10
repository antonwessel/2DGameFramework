using System.Diagnostics;

namespace GameFramework.ConsoleDemo.Logging;

public sealed class PrefixedConsoleTraceListener : TraceListener
{
    public override void Write(string? message)
    {
        Console.Write(message);
    }

    public override void WriteLine(string? message)
    {
        Console.WriteLine($"Trace: {message}");
    }
}

using GameFramework.Configuration;
using GameFramework.Models;
using GameFramework.Logging;
using System.Diagnostics;

// Testing XML-configuration

GameConfiguration config = GameConfigurationLoader.Load("Configuration/GameConfiguration.xml");

World world = new(config.MaxX, config.MaxY);

Console.WriteLine($"World MaxX: {world.MaxX}");
Console.WriteLine($"World MaxY: {world.MaxY}");
Console.WriteLine($"Difficulty: {config.Difficulty}");

// Testing logging

TextWriterTraceListener listener = new(Console.Out);

MyLogger.Instance.AddListener(listener);
MyLogger.Instance.Log("test log message");
Trace.Flush();

MyLogger.Instance.RemoveListener(listener);
MyLogger.Instance.Log("this should not appear");
Trace.Flush();
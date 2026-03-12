using GameFramework.Configuration;
using GameFramework.Models;
using GameFramework.Logging;
using System.Diagnostics;
using GameFramework.ConsoleDemo.Models;

// Test XML-configuration

GameConfiguration config = GameConfigurationLoader.Load("Configuration/GameConfiguration.xml");

World world = new(config.MaxX, config.MaxY);

Console.WriteLine($"World MaxX: {world.MaxX}");
Console.WriteLine($"World MaxY: {world.MaxY}");
Console.WriteLine($"Difficulty: {config.Difficulty}");

// Test logging

TextWriterTraceListener listener = new(Console.Out);

MyLogger.Instance.AddListener(listener);
MyLogger.Instance.Log("test log message");
Trace.Flush();

MyLogger.Instance.RemoveListener(listener);
MyLogger.Instance.Log("this should not appear");
Trace.Flush();

// Test template method + observer pattern

BasicCreature attacker = new("Bob", 125, new Position(5, 12));
attacker.AttackItems.Add(new AttackItem("Sword", 130, 7));

BasicCreature target = new("Frank", 125, new Position(6, 12));
target.DefenceItems.Add(new DefenceItem("Shield", 5));

ConsoleCreatureObserver observer = new();
target.AddObserver(observer);

int hitPointsBeforeHit = target.HitPoints;
int attackPower = attacker.Hit(target);
int actualDamageTaken = hitPointsBeforeHit - target.HitPoints;

Console.WriteLine(
    $"{attacker.Name} attacks {target.Name} with {attackPower} attack power. " +
    $"{target.Name} takes {actualDamageTaken} damage and now has {target.HitPoints} health left.");
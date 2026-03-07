using GameFramework.Configuration;
using GameFramework.Models;

GameConfiguration config = GameConfigurationLoader.Load("Configuration/GameConfiguration.xml");

World world = new(config.MaxX, config.MaxY);

Console.WriteLine($"World MaxX: {world.MaxX}");
Console.WriteLine($"World MaxY: {world.MaxY}");
Console.WriteLine($"Difficulty: {config.Difficulty}");
using GameFramework.Models;

namespace GameFramework.ConsoleDemo.Models;

public class ConsoleCreatureObserver : ICreatureObserver
{
    public void OnCreatureDied(Creature creature)
    {
        Console.WriteLine($"{creature.Name} died.");
    }

    public void OnCreatureHit(Creature creature)
    {
        Console.WriteLine($"{creature.Name} was hit. HitPoints: {creature.HitPoints}");
    }
}
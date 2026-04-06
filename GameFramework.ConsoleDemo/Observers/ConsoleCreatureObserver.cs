using GameFramework.Models;
using GameFramework.Observers;

namespace GameFramework.ConsoleDemo.Observers;

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

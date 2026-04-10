using GameFramework.Observers;
using GameFramework.Models;

namespace GameFramework.ConsoleDemo.Observers;

public class ConsoleHitObserver : ICreatureHitObserver
{
    public void OnCreatureHit(Creature creature)
    {
        Console.WriteLine($"Observer: {creature.Name} was hit. HitPoints: {creature.HitPoints}");
    }
}

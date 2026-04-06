using GameFramework.Models;

namespace GameFramework.Observers;

/// <summary>
/// Defines callbacks for creature death events.
/// </summary>
public interface ICreatureDeathObserver
{
    /// <summary>
    /// Called when a creature dies.
    /// </summary>
    /// <param name="creature">The creature that died.</param>
    void OnCreatureDied(Creature creature);
}

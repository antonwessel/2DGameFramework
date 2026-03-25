using GameFramework.Models;

namespace GameFramework.Observers;

/// <summary>
/// Defines callbacks for creature events.
/// </summary>
public interface ICreatureObserver
{
    /// <summary>
    /// Called when a creature is hit.
    /// </summary>
    /// <param name="creature">The creature that was hit.</param>
    void OnCreatureHit(Creature creature);

    /// <summary>
    /// Called when a creature dies.
    /// </summary>
    /// <param name="creature">The creature that died.</param>
    void OnCreatureDied(Creature creature);
}
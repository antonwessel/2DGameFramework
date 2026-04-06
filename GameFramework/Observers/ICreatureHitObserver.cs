using GameFramework.Models;

namespace GameFramework.Observers;

/// <summary>
/// Defines callbacks for creature hit events.
/// </summary>
public interface ICreatureHitObserver
{
    /// <summary>
    /// Called when a creature is hit.
    /// </summary>
    /// <param name="creature">The creature that was hit.</param>
    void OnCreatureHit(Creature creature);
}

namespace GameFramework.Models;

/// <summary>
/// Defines a world object that can apply loot behavior to a creature.
/// </summary>
public interface ILootableWorldObject
{
    /// <summary>
    /// Applies this object's loot effect to a creature.
    /// </summary>
    /// <param name="creature">The creature getting the loot.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="creature"/> is null.</exception>
    void ApplyLoot(Creature creature);
}

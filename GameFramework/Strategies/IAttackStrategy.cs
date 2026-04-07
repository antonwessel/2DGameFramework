using GameFramework.Models;

namespace GameFramework.Strategies;

/// <summary>
/// Defines how attack damage is calculated.
/// </summary>
public interface IAttackStrategy
{
    /// <summary>
    /// Calculates attack damage for a creature.
    /// </summary>
    /// <param name="creature">The creature to calculate damage for.</param>
    /// <returns>The calculated damage value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="creature"/> is null.</exception>
    int CalculateDamage(Creature creature);
}

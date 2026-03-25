using GameFramework.Models;

namespace GameFramework.Strategies;

/// <summary>
/// Calculates damage by summing attack item damage.
/// </summary>
public class SumAttackStrategy : IAttackStrategy
{
    /// <summary>
    /// Calculates attack damage for a creature.
    /// </summary>
    /// <param name="creature">The creature to calculate damage for.</param>
    public int CalculateDamage(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);
        return creature.AttackItems.Sum(item => item.Damage);
    }
}
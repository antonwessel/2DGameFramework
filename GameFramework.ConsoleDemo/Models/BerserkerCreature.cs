using GameFramework.Models;
using GameFramework.Strategies;

namespace GameFramework.ConsoleDemo.Models;

/// <summary>
/// Demo creature that overrides the template hook in <see cref="Creature"/>.
/// </summary>
public class BerserkerCreature : BasicCreature
{
    private readonly int _damageBonus;

    /// <summary>
    /// Creates a berserker creature with a fixed damage bonus.
    /// </summary>
    /// <param name="name">The creature name.</param>
    /// <param name="hitPoints">The starting hit points.</param>
    /// <param name="position">The starting position.</param>
    /// <param name="maxAttackItemWeight">The max total attack item weight.</param>
    /// <param name="damageBonus">The bonus damage added by the template hook.</param>
    public BerserkerCreature(string name, int hitPoints, Position position, int maxAttackItemWeight, int damageBonus = 5)
        : this(name, hitPoints, position, maxAttackItemWeight, new SumAttackStrategy(), damageBonus)
    {
    }

    /// <summary>
    /// Creates a berserker creature with a custom attack strategy and a fixed damage bonus.
    /// </summary>
    /// <param name="name">The creature name.</param>
    /// <param name="hitPoints">The starting hit points.</param>
    /// <param name="position">The starting position.</param>
    /// <param name="maxAttackItemWeight">The max total attack item weight.</param>
    /// <param name="attackStrategy">The strategy used to calculate attack damage.</param>
    /// <param name="damageBonus">The bonus damage added by the template hook.</param>
    public BerserkerCreature(
        string name,
        int hitPoints,
        Position position,
        int maxAttackItemWeight,
        IAttackStrategy attackStrategy,
        int damageBonus = 5)
        : base(name, hitPoints, position, maxAttackItemWeight, attackStrategy)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(damageBonus);
        _damageBonus = damageBonus;
    }

    /// <inheritdoc />
    protected override int AdjustDamage(int damage, Creature enemy)
    {
        return base.AdjustDamage(damage, enemy) + _damageBonus;
    }
}

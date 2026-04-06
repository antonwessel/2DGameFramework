using GameFramework.Strategies;

namespace GameFramework.Models;

/// <summary>
/// Simple creature implementation.
/// </summary>
public class BasicCreature : Creature
{
    /// <summary>
    /// Creates a basic creature.
    /// </summary>
    /// <param name="name">The creature name.</param>
    /// <param name="hitPoints">The starting hit points.</param>
    /// <param name="position">The starting position.</param>
    /// <param name="maxAttackItemWeight">The max total attack item weight.</param>
    public BasicCreature(string name, int hitPoints, Position position, int maxAttackItemWeight)
        : this(name, hitPoints, position, maxAttackItemWeight, new SumAttackStrategy())
    {
    }

    /// <summary>
    /// Creates a basic creature with a custom attack strategy.
    /// </summary>
    /// <param name="name">The creature name.</param>
    /// <param name="hitPoints">The starting hit points.</param>
    /// <param name="position">The starting position.</param>
    /// <param name="maxAttackItemWeight">The max total attack item weight.</param>
    /// <param name="attackStrategy">The strategy used to calculate attack damage.</param>
    public BasicCreature(
        string name,
        int hitPoints,
        Position position,
        int maxAttackItemWeight,
        IAttackStrategy attackStrategy)
        : base(name, hitPoints, position, maxAttackItemWeight, attackStrategy)
    {
    }
}

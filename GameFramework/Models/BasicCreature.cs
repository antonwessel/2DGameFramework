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
        : base(name, hitPoints, position, maxAttackItemWeight)
    {
    }
}
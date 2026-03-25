namespace GameFramework.Items;

/// <summary>
/// Represents a single attack item.
/// </summary>
public class AttackItem : IAttackItem
{
    /// <summary>
    /// Gets the item name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the damage value.
    /// </summary>
    public int Damage { get; }

    /// <summary>
    /// Gets the attack range.
    /// </summary>
    public int Range { get; }

    /// <summary>
    /// Gets the item weight.
    /// </summary>
    public int Weight { get; }

    /// <summary>
    /// Creates an attack item.
    /// </summary>
    /// <param name="name">The item name.</param>
    /// <param name="damage">The damage value.</param>
    /// <param name="range">The attack range.</param>
    /// <param name="weight">The item weight.</param>
    public AttackItem(string name, int damage, int range, int weight)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegative(damage);
        ArgumentOutOfRangeException.ThrowIfNegative(range);
        ArgumentOutOfRangeException.ThrowIfNegative(weight);

        Name = name;
        Damage = damage;
        Range = range;
        Weight = weight;
    }
}
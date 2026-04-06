namespace GameFramework.Items;

/// <summary>
/// Represents a defence item.
/// </summary>
public class DefenceItem : IDefenceItem
{
    /// <summary>
    /// Gets the item name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets how much damage the item blocks.
    /// </summary>
    public int DamageReduction { get; }

    /// <summary>
    /// Creates a defence item.
    /// </summary>
    /// <param name="name">The item name.</param>
    /// <param name="damageReduction">The damage reduction value.</param>
    public DefenceItem(string name, int damageReduction)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegative(damageReduction);

        Name = name;
        DamageReduction = damageReduction;
    }
}

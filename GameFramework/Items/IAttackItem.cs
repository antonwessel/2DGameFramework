namespace GameFramework.Items;

/// <summary>
/// Defines an item that can be used for attack.
/// </summary>
public interface IAttackItem
{
    /// <summary>
    /// Gets the item name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the damage value.
    /// </summary>
    int Damage { get; }

    /// <summary>
    /// Gets the attack range.
    /// </summary>
    int Range { get; }

    /// <summary>
    /// Gets the item weight.
    /// </summary>
    int Weight { get; }
}
namespace GameFramework.Items;

/// <summary>
/// Defines an item that can be used for defence.
/// </summary>
public interface IDefenceItem
{
    /// <summary>
    /// Gets the item name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets how much damage the item blocks.
    /// </summary>
    int DamageReduction { get; }
}

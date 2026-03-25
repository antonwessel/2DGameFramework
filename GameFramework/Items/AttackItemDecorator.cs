namespace GameFramework.Items;

/// <summary>
/// Base class for attack item decorators.
/// </summary>
public abstract class AttackItemDecorator : IAttackItem
{
    /// <summary>
    /// Gets the wrapped attack item.
    /// </summary>
    protected IAttackItem InnerAttackItem { get; }

    /// <summary>
    /// Wraps another attack item.
    /// </summary>
    /// <param name="innerAttackItem">The item to wrap.</param>
    protected AttackItemDecorator(IAttackItem innerAttackItem)
    {
        ArgumentNullException.ThrowIfNull(innerAttackItem);
        InnerAttackItem = innerAttackItem;
    }

    /// <summary>
    /// Gets the item name.
    /// </summary>
    public virtual string Name => InnerAttackItem.Name;

    /// <summary>
    /// Gets the damage value.
    /// </summary>
    public virtual int Damage => InnerAttackItem.Damage;

    /// <summary>
    /// Gets the attack range.
    /// </summary>
    public virtual int Range => InnerAttackItem.Range;

    /// <summary>
    /// Gets the item weight.
    /// </summary>
    public virtual int Weight => InnerAttackItem.Weight;
}
namespace GameFramework.Items;

/// <summary>
/// Adds extra damage to an attack item.
/// </summary>
public class BoostedAttackItem : AttackItemDecorator
{
    private readonly int _damageBoost;

    /// <summary>
    /// Creates a boosted attack item.
    /// </summary>
    /// <param name="innerAttackItem">The item to wrap.</param>
    /// <param name="damageBoost">The extra damage to add.</param>
    public BoostedAttackItem(IAttackItem innerAttackItem, int damageBoost) : base(innerAttackItem)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(damageBoost);
        _damageBoost = damageBoost;
    }

    /// <summary>
    /// Gets the boosted damage value.
    /// </summary>
    public override int Damage => base.Damage + _damageBoost;
}
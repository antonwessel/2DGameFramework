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
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="innerAttackItem"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="damageBoost"/> is negative.</exception>
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

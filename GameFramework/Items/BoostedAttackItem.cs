namespace GameFramework.Items;

public class BoostedAttackItem : AttackItemDecorator
{
    private readonly int _damageBoost;

    public BoostedAttackItem(IAttackItem innerAttackItem, int damageBoost) : base(innerAttackItem)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(damageBoost);
        _damageBoost = damageBoost;
    }

    public override int Damage => base.Damage + _damageBoost;
}
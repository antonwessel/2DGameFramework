namespace GameFramework.Items;

public abstract class AttackItemDecorator : IAttackItem
{
    protected IAttackItem InnerAttackItem { get; }

    protected AttackItemDecorator(IAttackItem innerAttackItem)
    {
        ArgumentNullException.ThrowIfNull(innerAttackItem);
        InnerAttackItem = innerAttackItem;
    }

    public virtual string Name => InnerAttackItem.Name;
    public virtual int Damage => InnerAttackItem.Damage;
    public virtual int Range => InnerAttackItem.Range;
}

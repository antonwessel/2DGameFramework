namespace GameFramework.Items;

public class AttackItem : IAttackItem
{
    public string Name { get; }
    public int Damage { get; }
    public int Range { get; }

    public AttackItem(string name, int damage, int range)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegative(damage);
        ArgumentOutOfRangeException.ThrowIfNegative(range);

        Name = name;
        Damage = damage;
        Range = range;
    }
}

namespace GameFramework.Items;

public class AttackItem : IAttackItem
{
    public string Name { get; }
    public int Damage { get; }
    public int Range { get; }
    public int Weight { get; }

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

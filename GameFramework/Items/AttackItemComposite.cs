namespace GameFramework.Items;

public class AttackItemComposite : IAttackItem
{
    private readonly List<IAttackItem> _attackItems;

    public string Name { get; }
    public int Damage => _attackItems.Sum(item => item.Damage);
    public int Range => _attackItems.Count == 0 ? 0 : _attackItems.Max(item => item.Range);
    public int Weight => _attackItems.Sum(item => item.Weight);

    public AttackItemComposite(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
        _attackItems = [];
    }

    public void AddItem(IAttackItem attackItem)
    {
        ArgumentNullException.ThrowIfNull(attackItem);
        _attackItems.Add(attackItem);
    }

    public void RemoveItem(IAttackItem attackItem)
    {
        ArgumentNullException.ThrowIfNull(attackItem);
        _attackItems.Remove(attackItem);
    }
}
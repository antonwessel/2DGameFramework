namespace GameFramework.Items;

/// <summary>
/// Represents a group of attack items as one item.
/// </summary>
public class AttackItemComposite : IAttackItem
{
    private readonly List<IAttackItem> _attackItems;

    /// <summary>
    /// Gets the composite name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the total damage from all items.
    /// </summary>
    public int Damage => _attackItems.Sum(item => item.Damage);

    /// <summary>
    /// Gets the highest range from all items.
    /// </summary>
    public int Range => _attackItems.Count == 0 ? 0 : _attackItems.Max(item => item.Range);

    /// <summary>
    /// Gets the total weight from all items.
    /// </summary>
    public int Weight => _attackItems.Sum(item => item.Weight);

    /// <summary>
    /// Creates an empty attack item group.
    /// </summary>
    /// <param name="name">The group name.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null, empty, or whitespace.</exception>
    public AttackItemComposite(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
        _attackItems = [];
    }

    /// <summary>
    /// Adds an item to the group.
    /// </summary>
    /// <param name="attackItem">The item to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="attackItem"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the composite would contain itself or create a cyclic composite structure.
    /// </exception>
    public void AddItem(IAttackItem attackItem)
    {
        ArgumentNullException.ThrowIfNull(attackItem);

        if (ReferenceEquals(attackItem, this))
        {
            throw new InvalidOperationException("An attack item composite cannot contain itself.");
        }

        if (attackItem is AttackItemComposite composite && composite.ContainsComposite(this))
        {
            throw new InvalidOperationException("An attack item composite cannot contain a cyclic composite.");
        }

        _attackItems.Add(attackItem);
    }

    /// <summary>
    /// Removes an item from the group.
    /// </summary>
    /// <param name="attackItem">The item to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="attackItem"/> is null.</exception>
    public void RemoveItem(IAttackItem attackItem)
    {
        ArgumentNullException.ThrowIfNull(attackItem);
        _attackItems.Remove(attackItem);
    }

    private bool ContainsComposite(AttackItemComposite target)
    {
        foreach (IAttackItem attackItem in _attackItems)
        {
            if (ReferenceEquals(attackItem, target))
            {
                return true;
            }

            if (attackItem is AttackItemComposite composite && composite.ContainsComposite(target))
            {
                return true;
            }
        }

        return false;
    }
}

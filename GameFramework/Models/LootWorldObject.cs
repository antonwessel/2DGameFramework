using GameFramework.Items;

namespace GameFramework.Models;

/// <summary>
/// Represents a lootable world object that gives either an attack item or a defence item.
/// </summary>
public class LootWorldObject : WorldObject, ILootableWorldObject
{
    private readonly IAttackItem? _attackItem;
    private readonly IDefenceItem? _defenceItem;

    /// <summary>
    /// Creates a loot object that gives an attack item.
    /// </summary>
    /// <param name="name">The object name.</param>
    /// <param name="position">The object position.</param>
    /// <param name="attackItem">The attack item granted when looted.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null, empty, or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="attackItem"/> is null.</exception>
    public LootWorldObject(string name, Position position, IAttackItem attackItem)
        : base(name, position, isRemovable: true)
    {
        _attackItem = attackItem ?? throw new ArgumentNullException(nameof(attackItem));
    }

    /// <summary>
    /// Creates a loot object that gives a defence item.
    /// </summary>
    /// <param name="name">The object name.</param>
    /// <param name="position">The object position.</param>
    /// <param name="defenceItem">The defence item granted when looted.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null, empty, or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="defenceItem"/> is null.</exception>
    public LootWorldObject(string name, Position position, IDefenceItem defenceItem)
        : base(name, position, isRemovable: true)
    {
        _defenceItem = defenceItem ?? throw new ArgumentNullException(nameof(defenceItem));
    }

    /// <summary>
    /// Applies this object's loot to a creature.
    /// </summary>
    /// <param name="creature">The creature getting the loot.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="creature"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the loot object does not contain any loot.</exception>
    public void ApplyLoot(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);

        if (_attackItem is not null)
        {
            creature.AddAttackItem(_attackItem);
        }
        else if (_defenceItem is not null)
        {
            creature.AddDefenceItem(_defenceItem);
        }
        else
        {
            throw new InvalidOperationException($"Loot object '{Name}' does not contain loot.");
        }
    }
}

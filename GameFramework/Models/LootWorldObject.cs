using GameFramework.Items;

namespace GameFramework.Models;

/// <summary>
/// Represents a lootable world object that gives either an attack item or a defence item.
/// </summary>
public class LootWorldObject : WorldObject
{
    private readonly IAttackItem? _attackItem;
    private readonly IDefenceItem? _defenceItem;

    /// <summary>
    /// Creates a loot object that gives an attack item.
    /// </summary>
    /// <param name="name">The object name.</param>
    /// <param name="position">The object position.</param>
    /// <param name="attackItem">The attack item granted when looted.</param>
    public LootWorldObject(string name, Position position, IAttackItem attackItem)
        : base(name, position, isLootable: true, isRemovable: true)
    {
        _attackItem = attackItem ?? throw new ArgumentNullException(nameof(attackItem));
    }

    /// <summary>
    /// Creates a loot object that gives a defence item.
    /// </summary>
    /// <param name="name">The object name.</param>
    /// <param name="position">The object position.</param>
    /// <param name="defenceItem">The defence item granted when looted.</param>
    public LootWorldObject(string name, Position position, IDefenceItem defenceItem)
        : base(name, position, isLootable: true, isRemovable: true)
    {
        _defenceItem = defenceItem ?? throw new ArgumentNullException(nameof(defenceItem));
    }

    /// <summary>
    /// Applies this object's loot to a creature.
    /// </summary>
    /// <param name="creature">The creature getting the loot.</param>
    public override void ApplyLoot(Creature creature)
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

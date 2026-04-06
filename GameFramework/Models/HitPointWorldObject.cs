namespace GameFramework.Models;

/// <summary>
/// Represents a consumable world object that changes hit points immediately when looted.
/// The effect is applied directly and the object is not added to a creature inventory.
/// </summary>
public class HitPointWorldObject : WorldObject
{
    /// <summary>
    /// Gets the hit-point change applied when the object is looted.
    /// Positive values heal and negative values damage.
    /// The object itself does not become an attack or defence item.
    /// </summary>
    public int HitPointDelta { get; }

    /// <summary>
    /// Creates a consumable hit-point effect world object.
    /// </summary>
    /// <param name="name">The object name.</param>
    /// <param name="position">The object position.</param>
    /// <param name="hitPointDelta">The hit-point change to apply.</param>
    /// <param name="isRemovable">Whether the object is removed after loot.</param>
    public HitPointWorldObject(string name, Position position, int hitPointDelta, bool isRemovable = true)
        : base(name, position, isLootable: true, isRemovable: isRemovable)
    {
        if (hitPointDelta == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(hitPointDelta), "HitPointDelta cannot be zero.");
        }

        HitPointDelta = hitPointDelta;
    }

    /// <summary>
    /// Applies the hit-point effect to a creature immediately.
    /// </summary>
    /// <param name="creature">The creature receiving the effect.</param>
    public override void ApplyLoot(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);

        if (HitPointDelta > 0)
        {
            creature.Heal(HitPointDelta);
            return;
        }

        creature.ReceiveHit(-HitPointDelta);
    }
}

namespace GameFramework.Models;

/// <summary>
/// Represents an object placed in the world.
/// </summary>
public class WorldObject
{
    /// <summary>
    /// Gets the object name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the object position.
    /// </summary>
    public Position Position { get; }

    /// <summary>
    /// Gets whether the object can be looted.
    /// </summary>
    public bool IsLootable { get; }

    /// <summary>
    /// Gets whether the object can be removed.
    /// </summary>
    public bool IsRemovable { get; }

    /// <summary>
    /// Creates a world object.
    /// </summary>
    /// <param name="name">The object name.</param>
    /// <param name="position">The object position.</param>
    /// <param name="isLootable">Whether the object can be looted.</param>
    /// <param name="isRemovable">Whether the object can be removed.</param>
    public WorldObject(string name, Position position, bool isLootable, bool isRemovable)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
        Position = position;
        IsLootable = isLootable;
        IsRemovable = isRemovable;
    }

    /// <summary>
    /// Applies this object's loot to a creature.
    /// </summary>
    /// <param name="creature">The creature getting the loot.</param>
    /// <remarks>The base implementation only validates the creature.</remarks>
    public virtual void ApplyLoot(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);
    }
}
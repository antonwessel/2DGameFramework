namespace GameFramework.Models;

/// <summary>
/// Represents the game world.
/// </summary>
public class World
{
    /// <summary>
    /// Gets the max X value for the world.
    /// </summary>
    public int MaxX { get; }

    /// <summary>
    /// Gets the max Y value for the world.
    /// </summary>
    public int MaxY { get; }

    /// <summary>
    /// Gets the creatures in the world.
    /// </summary>
    public List<Creature> Creatures { get; }

    /// <summary>
    /// Gets the world objects in the world.
    /// </summary>
    public List<WorldObject> WorldObjects { get; }

    /// <summary>
    /// Creates a world.
    /// </summary>
    /// <param name="maxX">The max X value.</param>
    /// <param name="maxY">The max Y value.</param>
    public World(int maxX, int maxY)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxX, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxY, 0);

        MaxX = maxX;
        MaxY = maxY;
        Creatures = [];
        WorldObjects = [];
    }

    /// <summary>
    /// Lets a creature loot an object that belongs to this world.
    /// </summary>
    /// <param name="creature">The creature looting the object.</param>
    /// <param name="worldObject">The object to loot.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the creature or object is not part of this world.
    /// </exception>
    public void LootObject(Creature creature, WorldObject worldObject)
    {
        ArgumentNullException.ThrowIfNull(creature);
        ArgumentNullException.ThrowIfNull(worldObject);

        if (!Creatures.Contains(creature))
        {
            throw new InvalidOperationException($"Creature '{creature.Name}' is not part of this world.");
        }

        if (!WorldObjects.Contains(worldObject))
        {
            throw new InvalidOperationException($"WorldObject '{worldObject.Name}' is not part of this world.");
        }

        creature.Loot(worldObject);

        if (worldObject.IsRemovable)
        {
            WorldObjects.Remove(worldObject);
        }
    }
}

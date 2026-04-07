namespace GameFramework.Models;

/// <summary>
/// Represents the game world.
/// </summary>
public class World
{
    private readonly List<Creature> _creatures;
    private readonly List<WorldObject> _worldObjects;

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
    public IReadOnlyList<Creature> Creatures => _creatures;

    /// <summary>
    /// Gets the world objects in the world.
    /// </summary>
    public IReadOnlyList<WorldObject> WorldObjects => _worldObjects;

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
        _creatures = [];
        _worldObjects = [];
    }

    /// <summary>
    /// Adds a creature to the world.
    /// </summary>
    /// <param name="creature">The creature to add.</param>
    public void AddCreature(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);

        if (_creatures.Contains(creature))
        {
            throw new InvalidOperationException($"Creature '{creature.Name}' is already part of this world.");
        }

        _creatures.Add(creature);
    }

    /// <summary>
    /// Adds a world object to the world.
    /// </summary>
    /// <param name="worldObject">The world object to add.</param>
    public void AddWorldObject(WorldObject worldObject)
    {
        ArgumentNullException.ThrowIfNull(worldObject);

        if (_worldObjects.Contains(worldObject))
        {
            throw new InvalidOperationException($"WorldObject '{worldObject.Name}' is already part of this world.");
        }

        _worldObjects.Add(worldObject);
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

        if (!_creatures.Contains(creature))
        {
            throw new InvalidOperationException($"Creature '{creature.Name}' is not part of this world.");
        }

        if (!_worldObjects.Contains(worldObject))
        {
            throw new InvalidOperationException($"WorldObject '{worldObject.Name}' is not part of this world.");
        }

        creature.Loot(worldObject);

        if (worldObject.IsRemovable)
        {
            _worldObjects.Remove(worldObject);
        }
    }
}

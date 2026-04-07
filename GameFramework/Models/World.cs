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
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="maxX"/> or <paramref name="maxY"/> is less than or equal to zero.
    /// </exception>
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
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="creature"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the creature is already part of the world or is outside the world bounds.
    /// </exception>
    public void AddCreature(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);

        if (_creatures.Contains(creature))
        {
            throw new InvalidOperationException($"Creature '{creature.Name}' is already part of this world.");
        }

        EnsureWithinBounds(creature.Position, "Creature", creature.Name);
        _creatures.Add(creature);
        GameFramework.Logging.MyLogger.Instance.Log(
            $"World: added creature '{creature.Name}' at ({creature.Position.X}, {creature.Position.Y}).");
    }

    /// <summary>
    /// Adds a world object to the world.
    /// </summary>
    /// <param name="worldObject">The world object to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="worldObject"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the object is already part of the world or is outside the world bounds.
    /// </exception>
    public void AddWorldObject(WorldObject worldObject)
    {
        ArgumentNullException.ThrowIfNull(worldObject);

        if (_worldObjects.Contains(worldObject))
        {
            throw new InvalidOperationException($"WorldObject '{worldObject.Name}' is already part of this world.");
        }

        EnsureWithinBounds(worldObject.Position, "WorldObject", worldObject.Name);
        _worldObjects.Add(worldObject);
        GameFramework.Logging.MyLogger.Instance.Log(
            $"World: added object '{worldObject.Name}' at ({worldObject.Position.X}, {worldObject.Position.Y}).");
    }

    /// <summary>
    /// Lets a creature loot an object that belongs to this world.
    /// </summary>
    /// <param name="creature">The creature looting the object.</param>
    /// <param name="worldObject">The object to loot.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="creature"/> or <paramref name="worldObject"/> is null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the creature or object is not part of this world or when the loot action is invalid.
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

    private void EnsureWithinBounds(Position position, string entityType, string entityName)
    {
        bool isWithinBounds =
            position.X >= 0 &&
            position.Y >= 0 &&
            position.X <= MaxX &&
            position.Y <= MaxY;

        if (!isWithinBounds)
        {
            throw new InvalidOperationException(
                $"{entityType} '{entityName}' at ({position.X}, {position.Y}) is outside world bounds 0..{MaxX}, 0..{MaxY}.");
        }
    }
}

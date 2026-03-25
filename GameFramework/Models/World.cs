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
}
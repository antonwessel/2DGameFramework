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
    /// Gets whether the object can be removed.
    /// </summary>
    public bool IsRemovable { get; }

    /// <summary>
    /// Creates a world object.
    /// </summary>
    /// <param name="name">The object name.</param>
    /// <param name="position">The object position.</param>
    /// <param name="isRemovable">Whether the object can be removed.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null, empty, or whitespace.</exception>
    public WorldObject(string name, Position position, bool isRemovable)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
        Position = position;
        IsRemovable = isRemovable;
    }
}

namespace GameFramework.Models;

/// <summary>
/// Represents a position in the world.
/// </summary>
public readonly struct Position
{
    /// <summary>
    /// Gets the X value.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Gets the Y value.
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Creates a position.
    /// </summary>
    /// <param name="x">The X value.</param>
    /// <param name="y">The Y value.</param>
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Adds two positions.
    /// </summary>
    /// <param name="a">The first position.</param>
    /// <param name="b">The second position.</param>
    /// <returns>A new position with the combined coordinates.</returns>
    public static Position operator +(Position a, Position b)
    {
        return new Position(a.X + b.X, a.Y + b.Y);
    }
}
namespace GameFramework.Configuration;

/// <summary>
/// Holds game configuration values.
/// </summary>
public class GameConfiguration
{
    /// <summary>
    /// Gets the max X value.
    /// </summary>
    public int MaxX { get; init; }

    /// <summary>
    /// Gets the max Y value.
    /// </summary>
    public int MaxY { get; init; }

    /// <summary>
    /// Gets the difficulty level.
    /// </summary>
    public Difficulty Difficulty { get; init; }
}
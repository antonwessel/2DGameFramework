using System.Xml.Linq;

namespace GameFramework.Configuration;

/// <summary>
/// Loads game configuration from XML.
/// </summary>
public static class GameConfigurationLoader
{
    /// <summary>
    /// Loads configuration from an XML file.
    /// </summary>
    /// <param name="filePath">The path to the configuration file.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when required elements are missing or the difficulty value is invalid.
    /// </exception>
    public static GameConfiguration Load(string filePath)
    {
        XDocument document = XDocument.Load(filePath);

        XElement? root = document.Root
            ?? throw new InvalidOperationException("Configuration file has no root element.");

        XElement? maxXElement = root.Element("MaxX")
            ?? throw new InvalidOperationException("Missing MaxX element.");

        XElement? maxYElement = root.Element("MaxY")
            ?? throw new InvalidOperationException("Missing MaxY element.");

        XElement? difficultyElement = root.Element("Difficulty")
            ?? throw new InvalidOperationException("Missing Difficulty element.");

        int maxX = int.Parse(maxXElement.Value);
        int maxY = int.Parse(maxYElement.Value);

        bool isValidDifficulty = Enum.TryParse(difficultyElement.Value, true, out Difficulty difficulty);
        if (!isValidDifficulty)
        {
            throw new InvalidOperationException($"Invalid difficulty value: {difficultyElement.Value}");
        }

        return new GameConfiguration
        {
            MaxX = maxX,
            MaxY = maxY,
            Difficulty = difficulty
        };
    }
}
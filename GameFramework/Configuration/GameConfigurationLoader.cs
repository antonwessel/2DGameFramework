using GameFramework.Logging;
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
    /// <returns>The loaded configuration.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="filePath"/> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the configuration file cannot be found.</exception>
    /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when the directory part of <paramref name="filePath"/> cannot be found.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when access to the configuration file is denied.</exception>
    /// <exception cref="System.IO.IOException">Thrown when an I/O error occurs while loading the configuration file.</exception>
    /// <exception cref="System.Xml.XmlException">Thrown when the configuration file does not contain valid XML.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when required elements are missing or when MaxX, MaxY, or Difficulty contains an invalid value.
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

        bool isValidMaxX = int.TryParse(maxXElement.Value, out int maxX);
        if (!isValidMaxX)
        {
            throw new InvalidOperationException($"Invalid MaxX value: {maxXElement.Value}");
        }

        bool isValidMaxY = int.TryParse(maxYElement.Value, out int maxY);
        if (!isValidMaxY)
        {
            throw new InvalidOperationException($"Invalid MaxY value: {maxYElement.Value}");
        }

        bool isValidDifficulty = Enum.TryParse(difficultyElement.Value, true, out Difficulty difficulty);
        if (!isValidDifficulty)
        {
            throw new InvalidOperationException($"Invalid difficulty value: {difficultyElement.Value}");
        }

        GameConfiguration configuration = new()
        {
            MaxX = maxX,
            MaxY = maxY,
            Difficulty = difficulty
        };

        MyLogger.Instance.Log($"Configuration loaded: bounds x 0..{maxX}, y 0..{maxY}, difficulty {difficulty}.");

        return configuration;
    }
}

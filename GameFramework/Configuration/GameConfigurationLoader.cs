using System.Xml.Linq;

namespace GameFramework.Configuration;

// This is the class responsible for actually loading the XML data
public static class GameConfigurationLoader
{
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

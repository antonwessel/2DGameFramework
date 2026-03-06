namespace GameFramework.Models;

public class World
{
    public int MaxX { get; }
    public int MaxY { get; }

    public List<Creature> Creatures { get; }
    public List<WorldObject> WorldObjects { get; }

    public World(int maxX, int maxY)
    {
        MaxX = maxX;
        MaxY = maxY;

        Creatures = [];
        WorldObjects = [];
    }
}

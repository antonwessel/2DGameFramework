namespace GameFramework.Models;

public class WorldObject
{
    public string Name { get; }
    public Position Position { get; set; }
    public bool IsLootable { get; }
    public bool IsRemovable { get; }

    public WorldObject(string name, Position position, bool isLootable, bool isRemovable)
    {
        Name = name;
        Position = position;
        IsLootable = isLootable;
        IsRemovable = isRemovable;
    }

    public virtual void ApplyLoot(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);
    }
}
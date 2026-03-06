namespace GameFramework.Models;

public class WorldObject
{
    public string Name { get; }
    public bool IsLootable { get; }
    public bool IsRemovable { get; }

    public WorldObject(string name, bool isLootable, bool isRemovable)
    {
        Name = name;
        IsLootable = isLootable;
        IsRemovable = isRemovable;
    }
}

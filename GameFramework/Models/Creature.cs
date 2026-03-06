namespace GameFramework.Models;

public class Creature
{
    public string Name { get; private set; }
    public int HitPoints { get; private set; }
    public Position Position { get; set; }
    public List<AttackItem> AttackItems { get; }
    public List<DefenceItem> DefenceItems { get; }

    public Creature(string name, int hitPoints, Position position)
    {
        Name = name;
        HitPoints = hitPoints;
        Position = position;

        AttackItems = [];
        DefenceItems = [];
    }

    public int Hit(Creature enemy)
    {
        return 0; // placeholder for now
    }

    public void ReceiveHit(int damage)
    {

    }

    public void Loot(WorldObject worldObject)
    {

    }
}

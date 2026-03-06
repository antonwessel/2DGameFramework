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
        ArgumentNullException.ThrowIfNull(enemy);
        int totalDamage = AttackItems.Sum(item => item.Damage);
        enemy.ReceiveHit(totalDamage);
        return totalDamage;
    }

    public void ReceiveHit(int damage)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(damage);
        int totalDefence = DefenceItems.Sum(item => item.DamageReduction);
        int finalDamage = damage - totalDefence;
        finalDamage = Math.Max(0, finalDamage); // If defence is greater than attack
        HitPoints -= finalDamage;
    }

    public void Loot(WorldObject worldObject)
    {
        ArgumentNullException.ThrowIfNull(worldObject);

        if (!worldObject.IsLootable)
        {
            throw new InvalidOperationException($"WorldObject '{worldObject.Name}' cannot be looted");
        }
    }
}

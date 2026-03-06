namespace GameFramework.Models;

public class AttackItem
{
    public string Name { get; }
    public int Damage { get; }
    public int Range { get; }

    public AttackItem(string name, int damage, int range)
    {
        Name = name;
        Damage = damage;
        Range = range;
    }
}

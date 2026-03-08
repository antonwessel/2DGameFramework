namespace GameFramework.Models;

public class BasicCreature : Creature
{
    public BasicCreature(string name, int hitPoints, Position position) : base(name, hitPoints, position)
    {
    }

    protected override int CalculateDamage()
    {
        return AttackItems.Sum(item => item.Damage);
    }
}

namespace GameFramework.Models;

public class BasicCreature : Creature
{
    public BasicCreature(string name, int hitPoints, Position position, int maxAttackItemWeight)
        : base(name, hitPoints, position, maxAttackItemWeight)
    {
    }
}
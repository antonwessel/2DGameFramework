namespace GameFramework.Models;

public class SumAttackStrategy : IAttackStrategy
{
    public int CalculateDamage(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);
        return creature.AttackItems.Sum(item => item.Damage);
    }
}

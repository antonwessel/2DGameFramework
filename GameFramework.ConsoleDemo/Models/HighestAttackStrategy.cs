using GameFramework.Models;

namespace GameFramework.ConsoleDemo.Models;

public class HighestAttackStrategy : IAttackStrategy
{
    public int CalculateDamage(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);

        if (creature.AttackItems.Count == 0)
        {
            return 0;
        }

        return creature.AttackItems.Max(item => item.Damage);
    }
}

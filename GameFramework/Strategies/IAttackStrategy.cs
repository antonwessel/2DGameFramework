using GameFramework.Models;

namespace GameFramework.Strategies;

public interface IAttackStrategy
{
    int CalculateDamage(Creature creature);
}

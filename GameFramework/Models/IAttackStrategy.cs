namespace GameFramework.Models;

public interface IAttackStrategy
{
    int CalculateDamage(Creature creature);
}

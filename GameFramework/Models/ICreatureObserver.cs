namespace GameFramework.Models;

public interface ICreatureObserver
{
    void OnCreatureHit(Creature creature);
    void OnCreatureDied(Creature creature);
}
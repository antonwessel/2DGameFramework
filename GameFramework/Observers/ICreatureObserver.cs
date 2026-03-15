using GameFramework.Models;

namespace GameFramework.Observers;

public interface ICreatureObserver
{
    void OnCreatureHit(Creature creature);
    void OnCreatureDied(Creature creature);
}
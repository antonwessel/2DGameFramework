namespace GameFramework.Observers;

/// <summary>
/// Defines callbacks for creature hit and death events.
/// </summary>
/// <remarks>
/// Convenience interface for observers that want both hit and death notifications.
/// </remarks>
public interface ICreatureObserver : ICreatureHitObserver, ICreatureDeathObserver
{
}

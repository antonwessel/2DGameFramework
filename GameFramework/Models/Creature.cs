namespace GameFramework.Models;

public abstract class Creature
{
    public string Name { get; private set; }
    public int HitPoints { get; private set; }
    public Position Position { get; set; }
    public List<AttackItem> AttackItems { get; }
    public List<DefenceItem> DefenceItems { get; }
    private readonly List<ICreatureObserver> _observers;

    public Creature(string name, int hitPoints, Position position)
    {
        Name = name;
        HitPoints = hitPoints;
        Position = position;
        AttackItems = [];
        DefenceItems = [];
        _observers = [];
    }

    // Template method
    public int Hit(Creature enemy)
    {
        ArgumentNullException.ThrowIfNull(enemy);
        int totalDamage = CalculateDamage();
        enemy.ReceiveHit(totalDamage);
        return totalDamage;
    }

    protected abstract int CalculateDamage(); // Hook

    public void ReceiveHit(int damage)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(damage);

        bool wasAliveBeforeHit = HitPoints > 0;

        int totalDefence = DefenceItems.Sum(item => item.DamageReduction);
        int finalDamage = damage - totalDefence;
        finalDamage = Math.Max(0, finalDamage); // If defence is greater than attack
        HitPoints -= finalDamage;

        NotifyHitObservers();

        if (wasAliveBeforeHit && HitPoints <= 0)
        {
            NotifyDiedObservers();
        }
    }

    public void Loot(WorldObject worldObject)
    {
        ArgumentNullException.ThrowIfNull(worldObject);

        if (!worldObject.IsLootable)
        {
            throw new InvalidOperationException($"WorldObject '{worldObject.Name}' cannot be looted");
        }
    }

    public void AddObserver(ICreatureObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        _observers.Add(observer);
    }

    public void RemoveObserver(ICreatureObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        _observers.Remove(observer);
    }

    private void NotifyHitObservers()
    {
        foreach (ICreatureObserver observer in _observers)
        {
            observer.OnCreatureHit(this);
        }
    }

    private void NotifyDiedObservers()
    {
        foreach (ICreatureObserver observer in _observers)
        {
            observer.OnCreatureDied(this);
        }
    }
}
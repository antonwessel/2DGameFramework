using GameFramework.Items;
using GameFramework.Observers;
using GameFramework.Strategies;

namespace GameFramework.Models;

public abstract class Creature
{
    public string Name { get; private set; }
    public int HitPoints { get; private set; }
    public Position Position { get; set; }
    public List<IAttackItem> AttackItems { get; }
    public List<DefenceItem> DefenceItems { get; }
    public IAttackStrategy AttackStrategy
    {
        get => _attackStrategy;
        set => _attackStrategy = value ?? throw new ArgumentNullException(nameof(value));
    }

    private readonly List<ICreatureObserver> _observers;
    private IAttackStrategy _attackStrategy;

    public Creature(string name, int hitPoints, Position position)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegative(hitPoints);

        Name = name;
        HitPoints = hitPoints;
        Position = position;
        AttackItems = [];
        DefenceItems = [];
        _observers = [];
        _attackStrategy = new SumAttackStrategy();
    }

    // Template method
    public int Hit(Creature enemy)
    {
        ArgumentNullException.ThrowIfNull(enemy);
        int totalDamage = CalculateDamage();
        enemy.ReceiveHit(totalDamage);
        return totalDamage;
    }

    // Hook
    protected virtual int CalculateDamage()
    {
        return AttackStrategy.CalculateDamage(this);
    }

    public void ReceiveHit(int damage)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(damage);

        bool wasAliveBeforeHit = HitPoints > 0;

        int totalDefence = DefenceItems.Sum(item => item.DamageReduction);
        int finalDamage = damage - totalDefence;
        finalDamage = Math.Max(0, finalDamage);
        HitPoints = Math.Max(0, HitPoints - finalDamage);

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

        if (Position.X != worldObject.Position.X || Position.Y != worldObject.Position.Y)
        {
            throw new InvalidOperationException(
                $"Creature '{Name}' is not at the same position as world object '{worldObject.Name}'");
        }

        worldObject.ApplyLoot(this);
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
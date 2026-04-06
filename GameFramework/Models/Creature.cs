using GameFramework.Items;
using GameFramework.Observers;
using GameFramework.Strategies;

namespace GameFramework.Models;

/// <summary>
/// Base class for creatures in the world.
/// </summary>
public abstract class Creature
{
    /// <summary>
    /// Gets the creature name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the current hit points.
    /// </summary>
    public int HitPoints { get; private set; }

    /// <summary>
    /// Gets or sets the creature position.
    /// </summary>
    public Position Position { get; set; }

    /// <summary>
    /// Gets the max total weight allowed for attack items.
    /// </summary>
    public int MaxAttackItemWeight { get; }

    /// <summary>
    /// Gets the attack items the creature carries.
    /// </summary>
    public IReadOnlyList<IAttackItem> AttackItems => _attackItems;

    /// <summary>
    /// Gets the defence items the creature uses.
    /// </summary>
    public IReadOnlyList<IDefenceItem> DefenceItems => _defenceItems;

    /// <summary>
    /// Gets or sets the strategy used to calculate damage.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the strategy is set to null.</exception>
    public IAttackStrategy AttackStrategy
    {
        get => _attackStrategy;
        set => _attackStrategy = value ?? throw new ArgumentNullException(nameof(value));
    }

    private readonly List<ICreatureHitObserver> _hitObservers;
    private readonly List<ICreatureDeathObserver> _deathObservers;
    private IAttackStrategy _attackStrategy;
    private readonly List<IAttackItem> _attackItems;
    private readonly List<IDefenceItem> _defenceItems;

    /// <summary>
    /// Creates a creature.
    /// </summary>
    /// <param name="name">The creature name.</param>
    /// <param name="hitPoints">The starting hit points.</param>
    /// <param name="position">The starting position.</param>
    /// <param name="maxAttackItemWeight">The max total attack item weight.</param>
    /// <param name="attackStrategy">The strategy used to calculate attack damage.</param>
    public Creature(
        string name,
        int hitPoints,
        Position position,
        int maxAttackItemWeight,
        IAttackStrategy attackStrategy)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegative(hitPoints);
        ArgumentOutOfRangeException.ThrowIfNegative(maxAttackItemWeight);
        ArgumentNullException.ThrowIfNull(attackStrategy);

        Name = name;
        HitPoints = hitPoints;
        Position = position;
        MaxAttackItemWeight = maxAttackItemWeight;
        _attackItems = [];
        _defenceItems = [];
        _hitObservers = [];
        _deathObservers = [];
        _attackStrategy = attackStrategy;
    }

    /// <summary>
    /// Hits another creature.
    /// </summary>
    /// <param name="enemy">The creature to hit.</param>
    /// <returns>The damage before defence is applied.</returns>
    public int Hit(Creature enemy)
    {
        ArgumentNullException.ThrowIfNull(enemy);
        int totalDamage = CalculateDamage();
        enemy.ReceiveHit(totalDamage);
        return totalDamage;
    }

    /// <summary>
    /// Calculates damage for a hit.
    /// </summary>
    /// <remarks>Override this to change how damage is calculated.</remarks>
    protected virtual int CalculateDamage()
    {
        return AttackStrategy.CalculateDamage(this);
    }

    /// <summary>
    /// Applies incoming damage to the creature.
    /// </summary>
    /// <param name="damage">The incoming damage value.</param>
    public void ReceiveHit(int damage)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(damage);

        bool wasAliveBeforeHit = HitPoints > 0;

        int totalDefence = _defenceItems.Sum(item => item.DamageReduction);
        int finalDamage = damage - totalDefence;
        finalDamage = Math.Max(0, finalDamage);
        HitPoints = Math.Max(0, HitPoints - finalDamage);

        NotifyHitObservers();

        if (wasAliveBeforeHit && HitPoints <= 0)
        {
            NotifyDiedObservers();
        }
    }

    /// <summary>
    /// Restores hit points to the creature.
    /// </summary>
    /// <param name="amount">The amount of hit points to restore.</param>
    public void Heal(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);
        HitPoints += amount;
    }

    /// <summary>
    /// Loots a world object at the current position.
    /// </summary>
    /// <param name="worldObject">The world object to loot.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the object cannot be looted or is at another position.
    /// </exception>
    public void Loot(WorldObject worldObject)
    {
        ArgumentNullException.ThrowIfNull(worldObject);

        if (!worldObject.IsLootable)
        {
            throw new InvalidOperationException($"WorldObject '{worldObject.Name}' cannot be looted");
        }

        if (!Position.Equals(worldObject.Position))
        {
            throw new InvalidOperationException(
                $"Creature '{Name}' is not at the same position as world object '{worldObject.Name}'");
        }

        worldObject.ApplyLoot(this);
    }

    /// <summary>
    /// Adds an attack item if the total weight fits.
    /// </summary>
    /// <param name="attackItem">The item to add.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the item would push the total weight past the limit.
    /// </exception>
    public void AddAttackItem(IAttackItem attackItem)
    {
        ArgumentNullException.ThrowIfNull(attackItem);

        int currentWeight = _attackItems.Sum(item => item.Weight);
        int newTotalWeight = currentWeight + attackItem.Weight;

        if (newTotalWeight > MaxAttackItemWeight)
        {
            throw new InvalidOperationException($"Creature '{Name}' cannot carry attack items above max weight {MaxAttackItemWeight}");
        }

        _attackItems.Add(attackItem);
    }

    /// <summary>
    /// Adds a defence item.
    /// </summary>
    /// <param name="defenceItem">The item to add.</param>
    public void AddDefenceItem(IDefenceItem defenceItem)
    {
        ArgumentNullException.ThrowIfNull(defenceItem);
        _defenceItems.Add(defenceItem);
    }

    /// <summary>
    /// Adds a hit observer.
    /// </summary>
    /// <param name="observer">The observer to add.</param>
    public void AddObserver(ICreatureHitObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);

        if (!_hitObservers.Contains(observer))
        {
            _hitObservers.Add(observer);
        }
    }

    /// <summary>
    /// Adds a death observer.
    /// </summary>
    /// <param name="observer">The observer to add.</param>
    public void AddObserver(ICreatureDeathObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);

        if (!_deathObservers.Contains(observer))
        {
            _deathObservers.Add(observer);
        }
    }

    /// <summary>
    /// Adds a combined observer.
    /// </summary>
    /// <param name="observer">The observer to add.</param>
    public void AddObserver(ICreatureObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        AddObserver((ICreatureHitObserver)observer);
        AddObserver((ICreatureDeathObserver)observer);
    }

    /// <summary>
    /// Removes a hit observer.
    /// </summary>
    /// <param name="observer">The observer to remove.</param>
    public void RemoveObserver(ICreatureHitObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        _hitObservers.Remove(observer);
    }

    /// <summary>
    /// Removes a death observer.
    /// </summary>
    /// <param name="observer">The observer to remove.</param>
    public void RemoveObserver(ICreatureDeathObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        _deathObservers.Remove(observer);
    }

    /// <summary>
    /// Removes a combined observer.
    /// </summary>
    /// <param name="observer">The observer to remove.</param>
    public void RemoveObserver(ICreatureObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        RemoveObserver((ICreatureHitObserver)observer);
        RemoveObserver((ICreatureDeathObserver)observer);
    }

    private void NotifyHitObservers()
    {
        foreach (ICreatureHitObserver observer in _hitObservers)
        {
            observer.OnCreatureHit(this);
        }
    }

    private void NotifyDiedObservers()
    {
        foreach (ICreatureDeathObserver observer in _deathObservers)
        {
            observer.OnCreatureDied(this);
        }
    }
}

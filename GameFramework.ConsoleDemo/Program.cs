using GameFramework.Configuration;
using GameFramework.ConsoleDemo.Logging;
using GameFramework.ConsoleDemo.Observers;
using GameFramework.ConsoleDemo.Strategies;
using GameFramework.Items;
using GameFramework.Logging;
using GameFramework.Models;
using System.Diagnostics;

PrefixedConsoleTraceListener listener = new();
Trace.AutoFlush = true;
MyLogger.Instance.AddListener(listener);
bool isFirstSection = true;

try
{
    GameConfiguration config = LoadConfigurationAndEnableTracing();
    RunCombatDemo();
    RunStrategyDemo();
    RunAttackItemPatternDemo();
    RunWorldObjectDemo(config);
}
finally
{
    MyLogger.Instance.RemoveListener(listener);
}

void PrintSection(string title)
{
    if (!isFirstSection)
    {
        Console.WriteLine();
    }

    isFirstSection = false;
    Console.WriteLine($"=== {title} ===");
}

GameConfiguration LoadConfigurationAndEnableTracing()
{
    PrintSection("Configuration and Tracing");

    string configurationPath = Path.Combine(AppContext.BaseDirectory, "Configuration", "GameConfiguration.xml");
    GameConfiguration config = GameConfigurationLoader.Load(configurationPath);

    Console.WriteLine($"World bounds: x 0..{config.MaxX}, y 0..{config.MaxY}");
    Console.WriteLine($"Difficulty: {config.Difficulty}");

    return config;
}

void RunCombatDemo()
{
    Position bobPosition = new(5, 12);
    Position closeTargetOffset = new(1, 0);
    Position distantTargetOffset = new(15, 0);
    Position frankPosition = bobPosition + closeTargetOffset;
    Position distantTargetPosition = bobPosition + distantTargetOffset;

    BasicCreature attacker = new("Bob", 125, bobPosition, 10);
    attacker.AddAttackItem(new AttackItem("Sword", 130, 7, 2));

    BasicCreature closeTarget = new("Frank", 125, frankPosition, 10);
    closeTarget.AddDefenceItem(new DefenceItem("Shield", 5));
    closeTarget.AddObserver(new ConsoleHitObserver());

    BasicCreature distantTarget = new("Distant Target", 100, distantTargetPosition, 10);

    PrintSection("Combat, Range, and Observer");
    Console.WriteLine(
        $"Setup: Bob is at (5, 12) with a sword (damage 130, range 7). Frank is at (6, 12) with 125 hit points, shield 5, and a console hit observer. Distant Target is at (20, 12).");

    int hitPointsBeforeHit = closeTarget.HitPoints;
    attacker.Hit(closeTarget);
    int damageAfterDefence = hitPointsBeforeHit - closeTarget.HitPoints;

    Console.WriteLine(
        $"Result: Frank goes from {hitPointsBeforeHit} to {closeTarget.HitPoints} hit points. Damage after defence: {damageAfterDefence}.");

    Console.WriteLine("Action: Bob tries to attack Distant Target at distance 15.");
    int outOfRangeAttack = attacker.Hit(distantTarget);
    Console.WriteLine(
        $"Result: Distant Target stays at {distantTarget.HitPoints} hit points because distance 15 is beyond range 7. Attack result: {outOfRangeAttack}.");
}

void RunStrategyDemo()
{
    PrintSection("Strategy");

    BasicCreature sumAttacker = new("Sum Bob", 100, new Position(1, 1), 10);
    sumAttacker.AddAttackItem(new AttackItem("Sword", 10, 5, 2));
    sumAttacker.AddAttackItem(new AttackItem("Axe", 20, 3, 4));

    BasicCreature highestAttacker = new("Highest Bob", 100, new Position(1, 1), 10, new HighestAttackStrategy());
    highestAttacker.AddAttackItem(new AttackItem("Sword", 10, 5, 2));
    highestAttacker.AddAttackItem(new AttackItem("Axe", 20, 3, 4));

    BasicCreature sumTarget = new("Training Dummy", 100, new Position(2, 1), 10);
    BasicCreature highestTarget = new("Sparring Dummy", 100, new Position(2, 1), 10);

    Console.WriteLine("Setup: both attackers use the same sword (10 damage) and axe (20 damage) from 1 tile away. Highest Bob uses the custom HighestAttackStrategy from the demo project.");

    int sumAttackPower = sumAttacker.Hit(sumTarget);
    int highestAttackPower = highestAttacker.Hit(highestTarget);

    Console.WriteLine($"Result: SumAttackStrategy gives {sumAttackPower} damage.");
    Console.WriteLine($"Result: HighestAttackStrategy gives {highestAttackPower} damage.");
}

void RunAttackItemPatternDemo()
{
    PrintSection("Attack Items");
    Console.WriteLine(
        "Setup: a plain sword has damage 10 and range 5, the boosted sword adds +5 damage, and the composite weapon set combines a sword and an axe.");

    IAttackItem plainSword = new AttackItem("Plain Sword", 10, 5, 2);
    IAttackItem boostedSword = new BoostedAttackItem(plainSword, 5);

    BasicCreature plainAttacker = new("Plain Bob", 100, new Position(2, 2), 10);
    plainAttacker.AddAttackItem(plainSword);

    BasicCreature boostedAttacker = new("Boosted Bob", 100, new Position(2, 2), 10);
    boostedAttacker.AddAttackItem(boostedSword);

    BasicCreature plainTarget = new("Plain Target", 100, new Position(2, 3), 10);
    BasicCreature boostedTarget = new("Boosted Target", 100, new Position(2, 3), 10);

    int plainDamage = plainAttacker.Hit(plainTarget);
    int boostedDamage = boostedAttacker.Hit(boostedTarget);

    AttackItemComposite weaponSet = new("Weapon Set");
    weaponSet.AddItem(new AttackItem("Composite Sword", 10, 5, 2));
    weaponSet.AddItem(new AttackItem("Composite Axe", 20, 3, 4));

    BasicCreature compositeAttacker = new("Composite Bob", 100, new Position(3, 3), 10);
    compositeAttacker.AddAttackItem(weaponSet);

    BasicCreature compositeTarget = new("Composite Target", 100, new Position(3, 4), 10);
    int compositeDamage = compositeAttacker.Hit(compositeTarget);

    Console.WriteLine($"Result: plain sword damage {plainDamage}, boosted sword damage {boostedDamage}.");
    Console.WriteLine(
        $"Result: composite totals are damage {compositeDamage}, range {weaponSet.Range}, weight {weaponSet.Weight}.");
}

void RunWorldObjectDemo(GameConfiguration config)
{
    PrintSection("World Objects and Loot");

    World world = new(config.MaxX, config.MaxY);
    Position sharedLootPosition = new(5, 5);
    Position rockOffset = new(1, 0);
    Position rockPosition = sharedLootPosition + rockOffset;

    BasicCreature looter = new("Luna", 40, sharedLootPosition, 10);
    AttackItem dagger = new("Dagger", 6, 2, 2);

    LootWorldObject chest = new("Starter Chest", sharedLootPosition, dagger);
    HitPointWorldObject healingPotion = new("Healing Potion", sharedLootPosition, 10);
    WorldObject rock = new("Rock", rockPosition, false);

    Console.WriteLine(
        "Setup: Luna, the chest, and the healing potion all start at (5, 5). A rock stays fixed at (6, 5). The chest gives a dagger and the potion restores 10 hit points.");

    world.AddCreature(looter);
    world.AddWorldObject(chest);
    world.AddWorldObject(healingPotion);
    world.AddWorldObject(rock);

    Console.WriteLine(
        $"Before loot: creatures {world.Creatures.Count}, world objects {world.WorldObjects.Count}, attack items {looter.AttackItems.Count}, hit points {looter.HitPoints}.");

    Console.WriteLine("Action: Luna loots the chest and then drinks the healing potion.");
    world.LootObject(looter, chest);
    world.LootObject(looter, healingPotion);

    Console.WriteLine(
        $"After loot: creatures {world.Creatures.Count}, world objects {world.WorldObjects.Count}, attack items {looter.AttackItems.Count}, hit points {looter.HitPoints}.");
    Console.WriteLine($"Result: Luna gains {dagger.Name}, heals to {looter.HitPoints} hit points, the chest and potion are removed, and the rock remains in the world.");
}

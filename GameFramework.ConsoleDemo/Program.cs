using GameFramework.Configuration;
using GameFramework.ConsoleDemo.Observers;
using GameFramework.ConsoleDemo.Strategies;
using GameFramework.Items;
using GameFramework.Logging;
using GameFramework.Models;
using System.Diagnostics;

Console.WriteLine("=== XML CONFIGURATION ===");

GameConfiguration config = GameConfigurationLoader.Load("Configuration/GameConfiguration.xml");
World world = new(config.MaxX, config.MaxY);

Console.WriteLine($"World MaxX: {world.MaxX}");
Console.WriteLine($"World MaxY: {world.MaxY}");
Console.WriteLine($"Difficulty: {config.Difficulty}");
Console.WriteLine();

Console.WriteLine("=== LOGGER ===");

TextWriterTraceListener listener = new(Console.Out);

MyLogger.Instance.AddListener(listener);
MyLogger.Instance.Log("test log message");
Trace.Flush();

MyLogger.Instance.RemoveListener(listener);
MyLogger.Instance.Log("this should not appear");
Trace.Flush();
Console.WriteLine();

Console.WriteLine("=== TEMPLATE + OBSERVER ===");

BasicCreature attacker = new("Bob", 125, new Position(5, 12), 10);
attacker.AddAttackItem(new AttackItem("Sword", 130, 7, 2));

BasicCreature target = new("Frank", 125, new Position(6, 12), 10);
target.AddDefenceItem(new DefenceItem("Shield", 5));

ConsoleCreatureObserver observer = new();
target.AddObserver(observer);

int hitPointsBeforeHit = target.HitPoints;
int attackPower = attacker.Hit(target);
int actualDamageTaken = hitPointsBeforeHit - target.HitPoints;

Console.WriteLine(
    $"{attacker.Name} attacks {target.Name} with {attackPower} attack power. " +
    $"{target.Name} takes {actualDamageTaken} damage and now has {target.HitPoints} health left.");
Console.WriteLine();

Console.WriteLine("=== STRATEGY ===");

BasicCreature sumAttacker = new("Sum Bob", 100, new Position(1, 1), 10);
sumAttacker.AddAttackItem(new AttackItem("Sword", 10, 5, 2));
sumAttacker.AddAttackItem(new AttackItem("Axe", 20, 3, 4));

BasicCreature highestAttacker = new("Highest Bob", 100, new Position(1, 1), 10, new HighestAttackStrategy());
highestAttacker.AddAttackItem(new AttackItem("Sword", 10, 5, 2));
highestAttacker.AddAttackItem(new AttackItem("Axe", 20, 3, 4));

BasicCreature sumTarget = new("Target 1", 100, new Position(1, 1), 10);
BasicCreature highestTarget = new("Target 2", 100, new Position(1, 1), 10);

int sumAttackPower = sumAttacker.Hit(sumTarget);
int highestAttackPower = highestAttacker.Hit(highestTarget);

Console.WriteLine($"SumAttackStrategy damage: {sumAttackPower}");
Console.WriteLine($"HighestAttackStrategy damage: {highestAttackPower}");
Console.WriteLine();

Console.WriteLine("=== DECORATOR ===");

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

Console.WriteLine($"Plain weapon damage: {plainDamage}");
Console.WriteLine($"Boosted weapon damage: {boostedDamage}");

Console.WriteLine();
Console.WriteLine("=== COMPOSITE ===");

IAttackItem compositeSword = new AttackItem("Composite Sword", 10, 5, 2);
IAttackItem compositeAxe = new AttackItem("Composite Axe", 20, 3, 4);

AttackItemComposite weaponSet = new("Weapon Set");
weaponSet.AddItem(compositeSword);
weaponSet.AddItem(compositeAxe);

BasicCreature compositeAttacker = new("Composite Bob", 100, new Position(3, 3), 10);
compositeAttacker.AddAttackItem(weaponSet);

BasicCreature compositeTarget = new("Composite Target", 100, new Position(3, 4), 10);

int compositeDamage = compositeAttacker.Hit(compositeTarget);

Console.WriteLine($"Composite weapon damage: {compositeDamage}");
Console.WriteLine($"Composite weapon range: {weaponSet.Range}");
Console.WriteLine($"Composite weapon weight: {weaponSet.Weight}");

Console.WriteLine();
Console.WriteLine("=== WEIGHT LIMIT ===");

BasicCreature weightLimitedCreature = new("Limited Bob", 100, new Position(4, 4), 5);

try
{
    weightLimitedCreature.AddAttackItem(new AttackItem("Heavy Sword", 10, 5, 3));
    weightLimitedCreature.AddAttackItem(new AttackItem("Heavy Axe", 20, 3, 4));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine();
Console.WriteLine("=== OPERATOR OVERLOAD ===");

Position firstPosition = new(2, 3);
Position secondPosition = new(4, 5);
Position combinedPosition = firstPosition + secondPosition;

Console.WriteLine(
    $"({firstPosition.X}, {firstPosition.Y}) + ({secondPosition.X}, {secondPosition.Y}) = " +
    $"({combinedPosition.X}, {combinedPosition.Y})");

Console.WriteLine();
Console.WriteLine("=== LOOT ===");

World lootWorld = new(10, 10);
Position lootPosition = new(5, 5);
BasicCreature looter = new("Luna", 100, lootPosition, 10);
AttackItem dagger = new("Dagger", 6, 2, 2);
DefenceItem leatherVest = new("Leather Vest", 3);

LootWorldObject chest = new(
    "Starter Chest",
    lootPosition,
    dagger);
LootWorldObject armorStand = new(
    "Armor Stand",
    lootPosition,
    leatherVest);

lootWorld.Creatures.Add(looter);
lootWorld.WorldObjects.Add(chest);
lootWorld.WorldObjects.Add(armorStand);

int worldObjectsBeforeLoot = lootWorld.WorldObjects.Count;

Console.WriteLine($"{looter.Name} starts with {looter.AttackItems.Count} attack items.");
Console.WriteLine($"{looter.Name} starts with {looter.DefenceItems.Count} defence items.");
Console.WriteLine($"World objects before loot: {worldObjectsBeforeLoot}");

lootWorld.LootObject(looter, chest);
Console.WriteLine($"{looter.Name} loots {chest.Name} and gets {dagger.Name}.");

lootWorld.LootObject(looter, armorStand);
Console.WriteLine($"{looter.Name} loots {armorStand.Name} and gets {leatherVest.Name}.");

int worldObjectsAfterLoot = lootWorld.WorldObjects.Count;

Console.WriteLine($"{looter.Name} now has {looter.AttackItems.Count} attack item: {dagger.Name}.");
Console.WriteLine($"{looter.Name} now has {looter.DefenceItems.Count} defence item: {leatherVest.Name}.");
Console.WriteLine($"World objects after loot: {worldObjectsAfterLoot}");

Console.WriteLine();
Console.WriteLine("=== CONSUMABLE WORLD EFFECTS ===");

World effectWorld = new(10, 10);
Position effectPosition = new(7, 7);
BasicCreature effectCreature = new("Mira", 40, effectPosition, 10);

HitPointWorldObject healingPotion = new("Healing Potion", effectPosition, 10);
HitPointWorldObject poisonFlask = new("Poison Flask", effectPosition, -15);

effectWorld.Creatures.Add(effectCreature);
effectWorld.WorldObjects.Add(healingPotion);
effectWorld.WorldObjects.Add(poisonFlask);

Console.WriteLine($"{effectCreature.Name} starts with {effectCreature.HitPoints} hit points.");
Console.WriteLine($"World objects before effects: {effectWorld.WorldObjects.Count}");

effectWorld.LootObject(effectCreature, healingPotion);
Console.WriteLine($"{effectCreature.Name} uses {healingPotion.Name} and immediately goes to {effectCreature.HitPoints} hit points.");

effectWorld.LootObject(effectCreature, poisonFlask);
Console.WriteLine($"{effectCreature.Name} uses {poisonFlask.Name} and immediately drops to {effectCreature.HitPoints} hit points.");

Console.WriteLine($"World objects after effects: {effectWorld.WorldObjects.Count}");

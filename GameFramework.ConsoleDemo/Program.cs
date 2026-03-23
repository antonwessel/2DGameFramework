using GameFramework.Configuration;
using GameFramework.ConsoleDemo.Models;
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
target.DefenceItems.Add(new DefenceItem("Shield", 5));

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

BasicCreature highestAttacker = new("Highest Bob", 100, new Position(1, 1), 10);
highestAttacker.AddAttackItem(new AttackItem("Sword", 10, 5, 2));
highestAttacker.AddAttackItem(new AttackItem("Axe", 20, 3, 4));
highestAttacker.AttackStrategy = new HighestAttackStrategy();

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
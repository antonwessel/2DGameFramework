namespace GameFramework.Items;

public interface IAttackItem
{
    string Name { get; }
    int Damage { get; }
    int Range { get; }
    int Weight { get; }
}
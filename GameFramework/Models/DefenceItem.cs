namespace GameFramework.Models;

public class DefenceItem
{
    public string Name { get; }
    public int DamageReduction { get; }

    public DefenceItem(string name, int damageReduction)
    {
        Name = name;
        DamageReduction = damageReduction;
    }
}

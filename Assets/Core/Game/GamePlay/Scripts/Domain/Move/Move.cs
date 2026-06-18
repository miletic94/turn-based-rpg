using System.Collections.Generic;

public class Move
{
    public int Id { get; }
    public string Name { get; }
    public MoveCategory Category;
    public string IconAddress { get; }
    public Cost Cost { get; }
    public List<HealthModifier> HealthModifiers { get; }
    public List<StatModifier> StatModifiers { get; }


    public Move(int id,
        string name,
        MoveCategory category,
        string iconAddress,
        Cost cost,
        List<HealthModifier> healthModifiers,
        List<StatModifier> statModifiers)
    {
        Id = id;
        Name = name;
        Category = category;
        IconAddress = iconAddress;
        Cost = cost;
        HealthModifiers = healthModifiers;
        StatModifiers = statModifiers;
    }
}
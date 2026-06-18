using System.Collections.Generic;

public class Move
{
    public int Id { get; }
    public string Name { get; }
    public string IconAddress { get; }
    public Cost Cost { get; }
    public List<HealthModifier> HealthModifiers { get; }
    public List<StatModifier> StatModifiers { get; }


    public Move(int id,
        string name,
        string iconAddress,
        Cost cost,
        List<HealthModifier> healthModifiers,
        List<StatModifier> statModifiers)
    {
        Id = id;
        Name = name;
        IconAddress = iconAddress;
        Cost = cost;
        HealthModifiers = healthModifiers;
        StatModifiers = statModifiers;
    }
}
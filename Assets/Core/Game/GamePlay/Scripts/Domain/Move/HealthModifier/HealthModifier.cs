public class HealthModifier : IDescribable
{
    public int Id { get; }
    public HealthModifierType Type { get; }
    public TargetType Target { get; }
    public HealthModifierValue Value { get; }
    public bool IsSource { get; }
    public string Description => Type == HealthModifierType.Damage ?
        $"Deals {Value.Description} damage to {Target}" :
        $"Heals {Target} for {Value.Description} amount";
    public HealthModifier(int id,
        HealthModifierType type,
        TargetType target,
        HealthModifierValue value,
        bool isSource)
    {
        Id = id;
        Type = type;
        Target = target;
        Value = value;
        IsSource = isSource;
    }


}
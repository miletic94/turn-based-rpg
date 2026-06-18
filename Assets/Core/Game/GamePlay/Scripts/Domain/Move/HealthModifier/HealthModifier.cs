public class HealthModifier
{
    public int Id { get; }
    public HealthModifierType Type { get; }
    public HealthModifierCategory Category { get; }
    public TargetType Target { get; }
    public HealthModifierValue Value { get; }
    public bool IsSource { get; }
    public HealthModifier(int id,
        HealthModifierType type,
        HealthModifierCategory category,
        TargetType target,
        HealthModifierValue value,
        bool isSource)
    {
        Id = id;
        Type = type;
        Category = category;
        Target = target;
        Value = value;
        IsSource = isSource;
    }

    public string GetTooltipDescription()
    {
        var value = Category == HealthModifierCategory.Referenced ?
            "the same" :
            $"{Value}";
        var action = Type == HealthModifierType.Damage ?
            $"Deals {value} damage to {Target}"
            : $"Heals {Target} for {value} amount";
        var scale = Category switch
        {
            HealthModifierCategory.Physical => $"Scales off attack, reduced by defense",
            HealthModifierCategory.Magic => $"Scales off magic",
            _ => "",
        };
        return $"{action}. {scale}.";
    }
}
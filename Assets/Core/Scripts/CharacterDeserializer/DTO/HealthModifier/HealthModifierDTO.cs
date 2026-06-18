public class HealthModifierDTO
{
    public int Id;
    public HealthModifierType Type;
    public TargetType Target;
    public HealthModifierValue Value;
    public bool IsSource;

    public HealthModifier ToHealthModifier()
    {
        return new HealthModifier(Id,
            Type,
            Target,
            Value,
            IsSource);
    }
}
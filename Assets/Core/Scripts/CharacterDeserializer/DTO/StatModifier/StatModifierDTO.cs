public class StatModifierDTO
{
    public int Id;
    public StatModifierType Type;
    public TargetType Target;
    public StatType TargetStat;
    public float Value;
    public int Duration;

    public StatModifier ToStatModifier()
    {
        return new StatModifier(Id, Type, Target, TargetStat, Value, Duration);
    }
}
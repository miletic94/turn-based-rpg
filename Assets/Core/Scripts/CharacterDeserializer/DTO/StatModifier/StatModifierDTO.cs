public class StatModifierDTO
{
    public int Id;
    public StatModifierType Type;
    public TargetType TargetType;
    public StatType TargetStat;
    public float Value;
    public int Duration;

    public StatModifier ToStatModifier()
    {
        return new StatModifier(Id, Type, TargetType, TargetStat, Value, Duration);
    }
}
public class StatModifier
{
    public int Id { get; }
    public StatModifierType Type;
    public TargetType Target { get; }
    public StatType TargetStat { get; }
    public float Value { get; }
    public int Duration { get; }
    public StatModifier(int id,
        StatModifierType type,
        TargetType target, StatType targetStat,
        float value,
        int duration)
    {
        Id = id;
        Type = type;
        Target = target;
        TargetStat = targetStat;
        Value = value;
        Duration = duration;
    }
}
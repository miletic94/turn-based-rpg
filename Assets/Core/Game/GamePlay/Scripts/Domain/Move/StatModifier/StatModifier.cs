public class StatModifier : IDescribable
{
    public int Id { get; }
    public StatModifierType Type;
    public TargetType Target { get; }
    public StatType TargetStat { get; }
    public float Value { get; }
    public int Duration { get; }
    public string Description =>
    $"{(Type == StatModifierType.Buff ? "Raises" : "Lowers")} {Target}'s {TargetStat} for {Value * 100}% for {Duration} turns";
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
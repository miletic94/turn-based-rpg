public class StatModifierEffect
{
    public StatModifierType Type { get; }
    public Combatant Target { get; }
    public StatType TargetStat { get; }
    public float Value { get; }
    public int Duration { get; }

    public StatModifierEffect(Combatant target, StatModifier statModifier)
    {
        var valueSign = statModifier.Type == StatModifierType.Debuff ? -1 : 1;

        Target = target;
        Type = statModifier.Type;
        TargetStat = statModifier.TargetStat;
        Value = valueSign * statModifier.Value;
        Duration = statModifier.Duration;
    }
    public StatModifierEffect(Combatant target, ActiveModifier activeModifier, StatModifier statModifier)
    {
        Target = target;
        Type = activeModifier.Type;
        TargetStat = activeModifier.TargetStat;
        Value = activeModifier.Value;
        Duration = activeModifier.RemainingDuration + statModifier.Duration;
    }
}
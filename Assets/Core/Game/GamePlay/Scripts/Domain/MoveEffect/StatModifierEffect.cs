public class StatModifierEffect
{
    public Combatant Target { get; }
    public StatType TargetStat { get; }
    public float Value { get; }
    public int Duration { get; }

    public StatModifierEffect(Combatant target, StatType targetStat, float value, int duration)
    {
        Target = target;
        TargetStat = targetStat;
        Value = value;
        Duration = duration;
    }
}
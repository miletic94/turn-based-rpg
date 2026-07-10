public class ActiveModifier
{
    public StatModifierType Type;
    public StatType TargetStat { get; }
    public float Value { get; }
    public int RemainingDuration { get; private set; }
    public bool IsExpired => RemainingDuration <= 0;

    public ActiveModifier(StatModifierEffect statModifierEffect)
    {
        Type = statModifierEffect.Type;
        TargetStat = statModifierEffect.TargetStat;
        Value = statModifierEffect.Value;
        RemainingDuration = statModifierEffect.Duration;
    }

    public void SubstractRemainingDuration()
    {
        RemainingDuration--;
    }
}
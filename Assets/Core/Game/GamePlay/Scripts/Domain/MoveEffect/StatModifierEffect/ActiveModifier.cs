public class ActiveModifier
{
    public StatModifierType Type;
    public StatType TargetStat { get; }
    public float Value { get; }
    public int RemainingDuration { get; private set; }
    public bool IsExpired => RemainingDuration <= 0;

    public ActiveModifier(StatModifierType type, StatType targetStat, float value, int duration)
    {
        Type = type;
        TargetStat = targetStat;
        Value = value;
        RemainingDuration = duration;
    }
    public void Stack(int duration)
    {
        RemainingDuration += duration;
    }

    public void SubstractRemainingDuration()
    {
        RemainingDuration--;
    }
}
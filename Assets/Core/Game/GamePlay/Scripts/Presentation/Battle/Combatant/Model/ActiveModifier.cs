public class ActiveModifier
{
    public StatType TargetStat { get; }
    public float Value { get; }
    public int RemainingDuration { get; private set; }

    public ActiveModifier(StatType targetStat, float value, int duration)
    {
        TargetStat = targetStat;
        Value = value;
        RemainingDuration = duration;
    }

    public void SubstractRemainingDuration()
    {
        RemainingDuration--;
    }
    public bool IsExpired => RemainingDuration <= 0;
}
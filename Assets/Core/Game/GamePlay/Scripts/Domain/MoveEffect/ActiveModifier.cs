public class ActiveModifier
{
    public ModifierType Type { get; }
    public StatType Stat { get; }
    public float Value { get; }
    public bool HasOccured { get; private set; }
    public int RemainingDuration { get; private set; }

    public ActiveModifier(ModifierType type, StatType stat, float value, int duration)
    {
        Type = type;
        Stat = stat;
        Value = value;
        HasOccured = false;
        RemainingDuration = duration;
    }

    public void SetHasOccured(bool hasOccured)
    {
        HasOccured = hasOccured;
    }

    public void SubstractRemainingDuration()
    {
        RemainingDuration--;
    }
    public bool IsExpired => RemainingDuration <= 0;
}
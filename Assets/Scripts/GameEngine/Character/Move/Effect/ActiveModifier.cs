public class ActiveModifier
{
    public ModifierType Type { get; }
    public StatType Stat { get; }
    public int Value { get; }
    public ModifierOccurance Occurance { get; }
    public bool HasOccured { get; private set; }
    public int RemainingDuration { get; private set; }

    public ActiveModifier(ModifierType type, StatType stat, int value, int duration)
    {
        Type = type;
        Stat = stat;
        Value = value;
        HasOccured = false;
        RemainingDuration = duration;
    }

    public void Tick()
    {
        if (!HasOccured) HasOccured = true;
        RemainingDuration--;
    }

    public bool IsExpired => RemainingDuration <= 0;
}
public class ActiveModifier
{
    public ModifierType Type { get; }
    public StatType Stat { get; }
    public int Value { get; }
    public int RemainingDuration { get; private set; }

    public ActiveModifier(ModifierType type, StatType stat, int value, int duration)
    {
        Type = type;
        Stat = stat;
        Value = value;
        RemainingDuration = duration;
    }

    public void Tick()
    {
        RemainingDuration--;
    }

    public bool IsExpired => RemainingDuration <= 0;
}
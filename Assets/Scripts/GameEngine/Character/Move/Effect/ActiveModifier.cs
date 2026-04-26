public class ActiveModifier
{
    public ModifierType Type { get; }
    public StatType Stat { get; }
    public float Value { get; }
    public ModifierTickBehavior TickBehavior { get; }
    public bool HasOccured { get; private set; }
    private int _baseDuration;
    public int RemainingDuration { get; private set; }
    public int DurationDelta => _baseDuration - RemainingDuration;

    public ActiveModifier(ModifierType type, StatType stat, float value, ModifierTickBehavior tickBehavior, int duration)
    {
        Type = type;
        Stat = stat;
        Value = value;
        HasOccured = false;
        _baseDuration = duration;
        RemainingDuration = duration;
        TickBehavior = tickBehavior;
    }

    public void Tick()
    {
        if (!HasOccured) HasOccured = true;
        RemainingDuration--;
    }

    public bool IsExpired => RemainingDuration <= 0;
}
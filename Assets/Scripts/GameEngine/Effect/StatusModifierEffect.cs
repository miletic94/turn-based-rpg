public class StatModifierEffect : IEffect
{
    public string? Id => null;
    public ModifierType Type;
    public TargetType Target { get; }
    public StatType Stat { get; }
    public int Value { get; }
    public int Duration { get; }

    public StatModifierEffect(TargetType target, StatType stat, int value, int duration)
    {
        Target = target;
        Stat = stat;
        Value = value;
        Duration = duration;
    }

    public void Execute(EffectContext context)
    {
        var target = context.ResolveTarget(Target);
        target.ApplyModifier(Type, Stat, Value, Duration);
    }
}
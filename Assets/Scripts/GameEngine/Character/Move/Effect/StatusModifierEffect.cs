using Newtonsoft.Json;

public class StatModifierEffect : IEffect
{
    public string Id { get; }
    public ModifierType Type;
    public TargetType Target { get; }
    public StatType Stat { get; }
    public IValue Value { get; }
    public ModifierTickBehavior TickBehavior { get; }
    public int Duration { get; }
    public bool IsSource { get; }
    public StatModifierEffect(string id, ModifierType type, TargetType target, StatType stat, IValue value, ModifierTickBehavior tickBehavior, int duration, bool isSource)
    {
        Id = id;
        Type = type;
        Target = target;
        Stat = stat;
        Value = value;
        TickBehavior = tickBehavior;
        Duration = duration;
        IsSource = isSource;
    }

    public void Execute(EffectContext context)
    {
        var target = context.ResolveTarget(Target);
        var value = Value.GetValue(context);

        if (IsSource)
            context.StoreResult(Id, value);

        target.ApplyModifier(Type, Stat, value, TickBehavior, Duration);
    }
}
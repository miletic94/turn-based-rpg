public class StatModifierEffect : IEffect
{
    public string Id { get; }
    public ModifierType Type;
    public TargetType Target { get; }
    public StatType Stat { get; }
    public IValue Value { get; }
    public ModifierOccurrence Occurrence { get; }
    public int Duration { get; }
    public bool IsSource { get; }

    public StatModifierEffect(string id, TargetType target, StatType stat, IValue value, ModifierOccurrence occurrence, int duration, bool isSource)
    {
        Id = id;
        Target = target;
        Stat = stat;
        Value = value;
        Occurrence = occurrence;
        Duration = duration;
        IsSource = isSource;
    }

    public void Execute(EffectContext context)
    {
        var target = context.ResolveTarget(Target);
        var value = Value.GetValue(context);

        if (IsSource)
            context.StoreResult(Id, value);

        target.ApplyModifier(Type, Stat, value, Duration);
    }
}
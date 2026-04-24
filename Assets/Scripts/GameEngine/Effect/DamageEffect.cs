public class DamageEffect : IEffect
{
    public string Id { get; }
    public TargetType Target { get; }
    public EffectCategory Category { get; }
    public IValue Value { get; }
    public bool IsSource { get; }

    public DamageEffect(string id, TargetType target, EffectCategory category, IValue value, bool isSource)
    {
        Id = id;
        Target = target;
        Category = category;
        Value = value;
        IsSource = isSource;
    }

    public void Execute(EffectContext context)
    {
        var target = context.ResolveTarget(Target);
        var value = Value.GetValue(context);

        if (IsSource)
            context.StoreResult(Id, value);

        target.ApplyDamage(value);
    }
}
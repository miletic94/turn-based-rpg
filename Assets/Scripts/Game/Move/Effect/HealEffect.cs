using Newtonsoft.Json;

public class HealEffect : IEffect
{
    public string Id { get; }
    public TargetType Target { get; }
    public IValue Value { get; }
    public bool IsSource { get; }

    public HealEffect(string id, TargetType target, IValue value, bool isSource)
    {
        Id = id;
        Target = target;
        Value = value;
        IsSource = isSource;
    }

    public void Execute(EffectContext context)
    {
        float value = Value.GetValue(context);
        var target = context.ResolveTarget(Target);
        var before = target.Health;

        if (IsSource)
            context.StoreResult(Id, value);

        target.Heal(value);

        context.Changes.Add(new StatChange
        {
            Target = target,
            Stat = StatType.Health,
            Before = before,
            After = target.Health,
            Source = this
        });
    }
}
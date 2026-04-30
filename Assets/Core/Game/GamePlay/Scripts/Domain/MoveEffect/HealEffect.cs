using Newtonsoft.Json;

public class HealEffect : IMoveEffect
{
    public string Id { get; }
    public TargetType Target { get; }
    public IMoveEffectValue Value { get; }
    public bool IsSource { get; }

    public HealEffect(string id, TargetType target, IMoveEffectValue value, bool isSource)
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

        if (IsSource)
            context.StoreResult(Id, value);

        target.Heal(value);
    }
}
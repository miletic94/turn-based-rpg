using Newtonsoft.Json;

public class HealEffect : IEffect
{
    public string Id { get; }
    public TargetType Target { get; }
    [JsonConverter(typeof(ValueConverter))]
    public IValue Value { get; }
    public bool IsSource { get; }

    [JsonConstructor]
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

        if (IsSource)
            context.StoreResult(Id, value);

        target.Heal(value);
    }
}
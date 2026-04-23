public class HealEffect : IEffect
{
    public string? Id => null;
    public TargetType Target { get; }

    public int? Value { get; }
    public string? RefId { get; }

    public HealEffect(TargetType target, int? value, string? refId)
    {
        Target = target;
        Value = value;
        RefId = refId;
    }

    public void Execute(EffectContext context)
    {
        int amount = Value ?? context.GetResult(RefId);

        var target = context.ResolveTarget(Target);
        target.Heal(amount);
    }
}
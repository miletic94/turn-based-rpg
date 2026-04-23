public class DamageEffect : IEffect
{
    public string? Id { get; }
    public TargetType Target { get; }
    public EffectCategory Category { get; }
    public int Value { get; }

    public DamageEffect(string? id, TargetType target, EffectCategory category, int value)
    {
        Id = id;
        Target = target;
        Category = category;
        Value = value;
    }

    public void Execute(EffectContext context)
    {
        var source = context.Source;
        var target = context.ResolveTarget(Target);

        int scaled = Category switch
        {
            EffectCategory.Physical => Value + source.Attack - target.Defense,
            EffectCategory.Magic => Value + source.Magic,
            _ => Value
        };

        context.StoreResult(Id, scaled);
        target.ApplyDamage(scaled);
    }
}
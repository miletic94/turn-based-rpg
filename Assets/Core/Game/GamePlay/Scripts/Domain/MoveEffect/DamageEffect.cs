public class DamageEffect : IMoveEffect
{
    public string Id { get; }
    public TargetType Target { get; }
    public EffectCategory Category { get; }
    public IMoveEffectValue Value { get; }
    public bool IsSource { get; }
    public DamageEffect(string id, TargetType target, EffectCategory category, IMoveEffectValue value, bool isSource)
    {
        Id = id;
        Target = target;
        Category = category;
        Value = value;
        IsSource = isSource;
    }
}
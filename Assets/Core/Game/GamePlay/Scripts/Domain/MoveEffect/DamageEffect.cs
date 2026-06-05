using System;

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

    public void Apply(Combatant target, float value)
    {
        target.SetHealth(Math.Max(0, target.Health - value));
    }
    public override string ToString()
    {
        return $"Deals damage to {Target}. {Value}";
    }
}
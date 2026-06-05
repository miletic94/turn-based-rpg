using System;
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

    public void Apply(Combatant target, float value)
    {
        target.SetHealth(Math.Min(target.Health + value, target.BaseHealth));
    }
    public override string ToString()
    {
        return $"Restores HP of {Target}. {Value}";
    }
}
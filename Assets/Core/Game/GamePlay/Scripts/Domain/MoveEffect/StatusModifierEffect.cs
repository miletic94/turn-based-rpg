public class StatModifierEffect : IMoveEffect
{
    public string Id { get; }
    public ModifierType Type;
    public TargetType Target { get; }
    public StatType Stat { get; }
    public IMoveEffectValue Value { get; }
    public ModifierTickBehavior TickBehavior { get; }
    public int Duration { get; }
    public bool IsSource { get; }
    public StatModifierEffect(string id, ModifierType type, TargetType target, StatType stat, IMoveEffectValue value, ModifierTickBehavior tickBehavior, int duration, bool isSource)
    {
        Id = id;
        Type = type;
        Target = target;
        Stat = stat;
        Value = value;
        TickBehavior = tickBehavior;
        Duration = duration;
        IsSource = isSource;
    }

    public void Apply(Combatant target, float value)
    {
        target.AddActiveModifier(new ActiveModifier(Type, Stat, value, Duration));
    }
    public override string ToString()
    {
        return $"{Type} {Target}'s {Stat} for {Duration} turns. Value: {(Value is FlatValue flatValue ? flatValue.BaseValue * 100 : Value)}";
    }
}
public class StatModifierEffect
{
    public StatModifierEffectType Type { get; }
    public Combatant Target { get; }
    public ActiveModifier ActiveModifier { get; }

    public StatModifierEffect(Combatant target, StatModifier statModifier, StatModifierEffectType type)
    {
        var valueSign = statModifier.Type == StatModifierType.Debuff ? -1 : 1;
        var value = valueSign * statModifier.Value;

        Type = type;
        Target = target;
        ActiveModifier = new ActiveModifier(statModifier.Type, statModifier.TargetStat, value, statModifier.Duration);
    }
    public StatModifierEffect(Combatant target, ActiveModifier activeModifer, StatModifierEffectType type)
    {
        Type = type;
        Target = target;
        ActiveModifier = activeModifer;
    }
}
public interface IEffectResult
{
    Combatant Target { get; }
}

public class DamageEffectResult : IEffectResult
{
    public Combatant Target { get; }
    public float Value { get; }
    public DamageEffectResult(Combatant target, float value)
    {
        Target = target;
        Value = value;
    }
}

public class HealEffectResult : IEffectResult
{
    public Combatant Target { get; }
    public float Value { get; }
    public HealEffectResult(Combatant target, float value)
    {
        Target = target;
        Value = value;
    }
}

public class StatModifierEffectResult : IEffectResult
{
    public Combatant Target { get; }
    public ActiveModifier Modifier { get; }

    public StatModifierEffectResult(Combatant target, ActiveModifier modifier)
    {
        Target = target;
        Modifier = modifier;
    }
}


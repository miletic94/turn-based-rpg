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
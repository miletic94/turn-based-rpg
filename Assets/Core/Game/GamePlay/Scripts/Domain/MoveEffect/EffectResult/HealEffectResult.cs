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

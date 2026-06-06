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

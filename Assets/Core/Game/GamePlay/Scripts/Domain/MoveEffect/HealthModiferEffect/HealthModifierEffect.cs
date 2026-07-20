public class HealthModifierEffect
{
    public Combatant Target { get; }
    public float Value { get; }

    public HealthModifierEffect(Combatant target, float value)
    {
        Target = target;
        Value = value;
    }
}
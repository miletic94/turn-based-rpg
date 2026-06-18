public class MoveExecutionService
{
    public MoveEffect Execute(MoveEffect moveEffect)
    {
        foreach (var modifierEffect in moveEffect.HealthModifierEffects)
        {
            var target = modifierEffect.Target;
            target.SetHealth(target.Health + modifierEffect.Value);
        }

        foreach (var modifierEffect in moveEffect.StatModifierEffects)
        {
            var target = modifierEffect.Target;
            target.AddActiveModifier(new ActiveModifier(
                modifierEffect.TargetStat,
                modifierEffect.Value,
                modifierEffect.Duration));
        }

        return moveEffect;
    }
}
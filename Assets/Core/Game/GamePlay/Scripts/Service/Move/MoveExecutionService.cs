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
            var targetStat = target.Stats[modifierEffect.TargetStat];

            int idx = targetStat.ActiveModifiers
                .FindIndex(modifier => modifier.Type == modifierEffect.Type &&
                            modifier.Value == modifierEffect.Value);
            if (idx == -1)
            {
                targetStat.AddActiveModifier(new ActiveModifier(modifierEffect));
            }
            else
            {
                targetStat.ReplaceActiveModifier(idx, new ActiveModifier(modifierEffect));
            }
        }

        return moveEffect;
    }
}

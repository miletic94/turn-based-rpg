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
            var activeModifier = modifierEffect.ActiveModifier;
            var targetStat = target.Stats[activeModifier.TargetStat];


            if (modifierEffect.Type == StatModifierEffectType.New)
            {
                targetStat.AddActiveModifier(modifierEffect.ActiveModifier);
            }
            else // StatMdifierEffectType.Stack
            {
                var idx = targetStat.ActiveModifiers.FindIndex(
                    modifier =>
                            modifier.Type == activeModifier.Type &&
                              modifier.Value == activeModifier.Value
                );
                if (idx == -1) throw new System.Exception("idx can't be -1; Since StatMdifierEffectType is Stacked, that must mean that ActiveModfier exists");
                targetStat.ReplaceActiveModifier(idx, modifierEffect.ActiveModifier);
            }
        }

        return moveEffect;
    }
}

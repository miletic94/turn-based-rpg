using System;
public class MoveEffectExecutionService
{
    public void Execute(IMoveEffect effect, EffectContext context)
    {
        var target = context.ResolveTarget(effect.Target);

        float value = effect.Value.GetValue(context);

        if (effect.IsSource)
            context.StoreResult(effect.Id, value);

        switch (effect)
        {
            case DamageEffect:
                target.ApplyDamage(value);
                break;

            case HealEffect:
                target.Heal(value);
                break;

            case StatModifierEffect modifierEffect:
                target.ApplyModifier(
                modifierEffect.Type,
                modifierEffect.Stat,
                value,
                modifierEffect.TickBehavior,
                modifierEffect.Duration);
                break;

            default:
                throw new Exception(
                    $"Unknown effect type: {effect.GetType().Name}");
        }
    }
}
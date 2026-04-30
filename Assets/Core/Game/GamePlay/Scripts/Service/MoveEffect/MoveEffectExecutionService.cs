using System;

public class MoveEffectExecutionService
{
    public void Execute(IMoveEffect effect, EffectContext context)
    {
        switch (effect)
        {
            case DamageEffect damageEffect:
                ExecuteDamage(damageEffect, context);
                break;

            case HealEffect healEffect:
                ExecuteHeal(healEffect, context);
                break;

            case StatModifierEffect modifierEffect:
                ExecuteStatModifier(modifierEffect, context);
                break;

            default:
                throw new Exception(
                    $"Unknown effect type: {effect.GetType().Name}");
        }
    }

    private void ExecuteDamage(
        DamageEffect effect,
        EffectContext context)
    {
        var target = context.ResolveTarget(effect.Target);

        float value = effect.Value.GetValue(context);

        if (effect.IsSource)
            context.StoreResult(effect.Id, value);

        target.ApplyDamage(value);
    }

    private void ExecuteHeal(
        HealEffect effect,
        EffectContext context)
    {
        var target = context.ResolveTarget(effect.Target);

        float value = effect.Value.GetValue(context);

        if (effect.IsSource)
            context.StoreResult(effect.Id, value);

        target.Heal(value);
    }

    private void ExecuteStatModifier(
        StatModifierEffect effect,
        EffectContext context)
    {
        var target = context.ResolveTarget(effect.Target);

        float value = effect.Value.GetValue(context);

        if (effect.IsSource)
            context.StoreResult(effect.Id, value);

        target.ApplyModifier(
            effect.Type,
            effect.Stat,
            value,
            effect.TickBehavior,
            effect.Duration);
    }
}
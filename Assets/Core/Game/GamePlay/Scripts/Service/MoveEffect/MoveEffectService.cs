using System;
using System.Linq;
using UnityEngine;
public class MoveEffectService
{
    public void Apply(IMoveEffect effect, EffectContext context)
    {
        var target = context.ResolveTarget(effect.Target);

        float value = effect.Value.Get(context);

        if (effect.IsSource)
            context.StoreResult(effect.Id, value);

        switch (effect)
        {
            case DamageEffect:
                ApplyDamage(target, value);
                break;

            case HealEffect:
                Heal(target, value);
                break;

            case StatModifierEffect modifierEffect:
                ApplyModifier(
                    new ActiveModifier(
                        modifierEffect.Type,
                        modifierEffect.Stat,
                        value,
                        modifierEffect.Duration),
                    target);
                break;

            default:
                throw new Exception(
                    $"Unknown effect type: {effect.GetType().Name}");
        }
    }

    public void ApplyDamage(Combatant target, float damage)
    {
        target.SetHealth(Math.Max(0, target.Health - damage));
    }

    public void Heal(Combatant target, float amount)
    {
        target.SetHealth(Math.Min(target.Health + amount, target.BaseHealth));
    }

    public void ApplyModifier(ActiveModifier modifier, Combatant target)
    {
        target.AddActiveModifier(modifier);
    }
}
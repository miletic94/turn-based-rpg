using System;
using System.Collections.Generic;

public class MoveEffectCalculationService
{
    public MoveEffect Calculate(Move move, Combatant actor, Combatant target)
    {
        var healthModifierEffects = CalculateHealthModifierEffects(move, actor, target);
        var statModifierEffects = CalculateStatModifierEffects(move, actor, target);

        return new MoveEffect(healthModifierEffects, statModifierEffects);
    }
    private List<HealthModifierEffect> CalculateHealthModifierEffects(Move move, Combatant actor, Combatant target)
    {
        EffectContext context = new EffectContext();
        List<HealthModifierEffect> healthModifierEffects = new();
        foreach (var modifier in move.HealthModifiers)
        {
            var targetCombatant = modifier.Target == TargetType.User ?
                actor : target;

            float baseValue = ResolveHealthModifierValue(modifier, context);

            float scaledValue = ApplyMoveScaling(
                baseValue,
                move.Category,
                actor,
                target);

            float valueSign = modifier.Type == HealthModifierType.Damage ?
                -1 : 1;

            float finalValue = scaledValue * valueSign;

            var effect = new HealthModifierEffect(targetCombatant, finalValue);

            if (modifier.IsSource)
            {
                context.StoreResult(modifier.Id, baseValue);
            }

            healthModifierEffects.Add(effect);
        }
        return healthModifierEffects;
    }
    private float ResolveHealthModifierValue(
        HealthModifier modifier,
        EffectContext context)
    {
        return modifier.Value.Type switch
        {
            HealthModifierValueType.Scaled =>
                modifier.Value.BaseValue
                    ?? throw new Exception("Modifier with Value.Type = Scaled needs BaseValue to be assigned"),

            HealthModifierValueType.Referenced =>
                context.GetResult(
                    modifier.Value.SourceId
                        ?? throw new Exception("Modifier with Value.Type = Referenced needs SourceId to be assigned")),

            _ => throw new Exception($"Modifier value type {modifier.Value.Type} not recognized")
        };
    }
    private float ApplyMoveScaling(float baseValue,
        MoveCategory moveCategory,
        Combatant actor,
        Combatant target)
    {
        return moveCategory switch
        {
            MoveCategory.Physical =>
                ApplyPhysicalScaling(baseValue, actor, target),

            MoveCategory.Magic =>
                ApplyMagicScaling(baseValue, actor),

            _ => throw new Exception($"Move category {moveCategory} not recognized")
        };
    }
    private float ApplyPhysicalScaling(float baseValue, Combatant actor, Combatant target)
    {
        var attack = actor.Stats.GetStat(StatType.Attack).GetCurrentValue();
        var defense = target.Stats.GetStat(StatType.Defense).GetCurrentValue();
        var doubleResult = baseValue * Math.Pow(attack, 2) / (attack + defense);
        return (float)doubleResult;
    }
    private float ApplyMagicScaling(float baseValue, Combatant actor)
    {
        var magic = actor.Stats.GetStat(StatType.Magic).GetCurrentValue();
        return baseValue * magic;
    }
    private List<StatModifierEffect> CalculateStatModifierEffects(Move move, Combatant actor, Combatant target)
    {
        List<StatModifierEffect> statModifierEffects = new();
        foreach (var modifier in move.StatModifiers)
        {
            var effect = CalculateStatModifierEffect(modifier, actor, target);
            statModifierEffects.Add(effect);
        }
        return statModifierEffects;
    }

    private StatModifierEffect CalculateStatModifierEffect(StatModifier statModifier, Combatant actor, Combatant target)
    {
        var targetCombatant = statModifier.Target == TargetType.User ?
            actor : target;
        var targetStat = targetCombatant.Stats.GetStat(statModifier.TargetStat);

        var activeModifier = targetStat.ActiveModifiers
            .Find(modifier => modifier.Type == statModifier.Type &&
                              modifier.Value == statModifier.Value);
        if (activeModifier == null)
            return new StatModifierEffect(targetCombatant, statModifier);
        else
            return new StatModifierEffect(targetCombatant, activeModifier);
    }
}
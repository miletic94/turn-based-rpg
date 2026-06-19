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
    private List<HealthModifierEffect> CalculateHealthModifierEffects(Move move, Combatant source, Combatant target)
    {
        EffectContext context = new EffectContext();
        List<HealthModifierEffect> healthModifierEffects = new();
        foreach (var modifier in move.HealthModifiers)
        {
            var targetCombatant = modifier.Target == TargetType.User ?
                source : target;

            float baseValue = ResolveHealthModifierValue(modifier, context);

            float scaledValue = ApplyMoveScaling(
                baseValue,
                move.Category,
                source,
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
        Combatant source,
        Combatant target)
    {
        return moveCategory switch
        {
            MoveCategory.Physical =>
                ApplyPhysicalScaling(baseValue, source, target),

            MoveCategory.Magic =>
                ApplyMagicScaling(baseValue, source),

            _ => throw new Exception($"Move category {moveCategory} not recognized")
        };
    }
    private float ApplyPhysicalScaling(float baseValue, Combatant source, Combatant target)
    {
        var attack = source.Stats.GetStat(StatType.Attack);
        var defense = target.Stats.GetStat(StatType.Defense);
        var doubleResult = baseValue * Math.Pow(attack, 2) / (attack + defense);
        return (float)doubleResult;
    }
    private float ApplyMagicScaling(float baseValue, Combatant source)
    {
        var magic = source.Stats.GetStat(StatType.Magic);
        return baseValue * magic;
    }
    private List<StatModifierEffect> CalculateStatModifierEffects(Move move, Combatant source, Combatant target)
    {
        List<StatModifierEffect> statModifierEffects = new();
        foreach (var modifier in move.StatModifiers)
        {
            var effect = CalculateStatModifierEffect(modifier, source, target);
            statModifierEffects.Add(effect);
        }
        return statModifierEffects;
    }

    private StatModifierEffect CalculateStatModifierEffect(StatModifier statModifier, Combatant source, Combatant target)
    {

        var targetCombatant = statModifier.Target == TargetType.User ?
            source : target;
        var valueSign = statModifier.Type == StatModifierType.Debuff ?
            -1 : 1;
        var value = valueSign * statModifier.Value;
        return new StatModifierEffect(targetCombatant, statModifier.TargetStat, value, statModifier.Duration);
    }
}
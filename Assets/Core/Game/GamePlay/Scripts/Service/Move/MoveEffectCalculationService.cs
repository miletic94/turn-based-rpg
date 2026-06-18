using System;
using System.Collections.Generic;

public class MoveEffectCalculationService
{
    public MoveEffect Calculate(Move move, Combatant source, Combatant target)
    {
        var healthModifierEffects = CalculateHealthModifierEffects(move.HealthModifiers, source, target);
        var statModifierEffects = CalculateStatModifierEffects(move.StatModifiers, source, target);

        return new MoveEffect(healthModifierEffects, statModifierEffects);
    }
    private List<HealthModifierEffect> CalculateHealthModifierEffects(List<HealthModifier> healthModifiers, Combatant source, Combatant target)
    {
        EffectContext context = new EffectContext();
        List<HealthModifierEffect> healthModifierEffects = new();

        foreach (var modifier in healthModifiers)
        {
            var effect = CalculateHealthModifierEffect(modifier, source, target, context);
            healthModifierEffects.Add(effect);
        }

        return healthModifierEffects;
    }
    private HealthModifierEffect CalculateHealthModifierEffect(
        HealthModifier healthModifier,
        Combatant source,
        Combatant target,
        EffectContext context)
    {
        var targetCombatant = healthModifier.Target == TargetType.User ?
            source : target;
        var valueSign = healthModifier.Type == HealthModifierType.Damage ?
            -1 : 1;
        var absoluteValue = CalculateHealthModifierValue(healthModifier, source, target, context);
        var value = valueSign * absoluteValue;

        if (healthModifier.IsSource)
        {
            context.StoreResult(healthModifier.Id, absoluteValue);
        }
        return new HealthModifierEffect(targetCombatant, value);
    }

    private float CalculateHealthModifierValue(
        HealthModifier healthModifier,
        Combatant source,
        Combatant target,
        EffectContext context)
    {
        return healthModifier.Category switch
        {
            HealthModifierCategory.Physical => CalculatePhysical(healthModifier, source, target),
            HealthModifierCategory.Magic => CalculateMagic(healthModifier, source),
            HealthModifierCategory.Referenced => CalculateReferenced(healthModifier, context),
            _ => throw new Exception($"Health modifier category {healthModifier.Category} not recognized")
        };
    }

    private float CalculatePhysical(
        HealthModifier modifier,
        Combatant source,
        Combatant target)
    {
        float baseValue = modifier.Value.BaseValue
            ?? throw new Exception("Physical modifier requires BaseValue");

        float attack = source.Stats.GetStat(StatType.Attack);
        float defense = target.Stats.GetStat(StatType.Defense);

        return baseValue * (attack * attack) / (attack + defense);
    }

    private float CalculateMagic(
        HealthModifier modifier,
        Combatant source)
    {
        float baseValue = modifier.Value.BaseValue
            ?? throw new Exception("Magic modifier requires BaseValue");

        float magic = source.Stats.GetStat(StatType.Magic);

        return baseValue * magic;
    }

    private float CalculateReferenced(
    HealthModifier modifier,
    EffectContext context)
    {
        int sourceId = modifier.Value.SourceId
            ?? throw new Exception("Referenced modifier requires SourceId");

        return context.GetResult(sourceId);
    }

    private List<StatModifierEffect> CalculateStatModifierEffects(List<StatModifier> statModifiers, Combatant source, Combatant target)
    {
        List<StatModifierEffect> statModifierEffects = new();

        foreach (var modifier in statModifiers)
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
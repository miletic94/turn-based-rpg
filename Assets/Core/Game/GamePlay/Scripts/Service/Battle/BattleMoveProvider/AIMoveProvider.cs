using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct MoveEvaluation
{
    public Move Move { get; private set; }
    public float Evaluation { get; private set; }
    public MoveEvaluation(Move move, float evaluation)
    {
        Move = move;
        Evaluation = evaluation;
    }
}

public class AIMoveProvider : IMoveProvider
{
    MoveEffectCalculationService _calculationService;
    // This field controls how strongly AI values health (currently both actor's and target's)
    // It can be read from design data in the future. 
    // In future we might want to control aggression and defensivnes independently
    private float _healthModifierMultiplier = 2f;
    // This field controls at which current / base health ratio AI will assume that health is critical
    // Currently it is for both damage and heal
    // If health is low health modifier will be valued more
    float _criticalHealthRatio = 0.25f;
    float _criticalHealthMultiplier = 1.5f;
    private const int PERCENT_SCALE = 100;
    public AIMoveProvider(MoveEffectCalculationService calculationService)
    {
        _calculationService = calculationService;
    }
    public async Awaitable<Move> GetMove(
           BattleContext context)
    {
        var moves = context.CurrentActor.Moves;
        List<MoveEvaluation> moveEvaluations = new();

        var actor = context.CurrentActor;
        var target = context.CurrentTarget;
        foreach (var move in moves)
        {
            var moveEffect = _calculationService.Calculate(move, actor, target);
            var healthModEval = EvaluateHealthModifiers(moveEffect.HealthModifierEffects);
            var statModEval = EvaluateStatModifiers(moveEffect.StatModifierEffects, actor, target, context.TurnNumber);
            var evaluation = healthModEval + statModEval;
            moveEvaluations.Add(new MoveEvaluation(move, evaluation));
        }
        var rankedMoves = moveEvaluations
            .OrderByDescending(x => x.Evaluation)
            .ToList();

        var debuggingString = "RANKING\n";
        foreach (var eval in rankedMoves)
        {
            debuggingString += $"{eval.Move.Name} : {eval.Evaluation}\n";
        }
        UnityEngine.Debug.Log(debuggingString);
        return rankedMoves[0].Move;
    }
    private float EvaluateHealthModifiers(List<HealthModifierEffect> healthModifiers)
    {
        float finalEvaluation = 0f;
        foreach (var effect in healthModifiers)
        {
            var currentHealth = effect.Target.Health;
            var baseHealth = effect.Target.BaseHealth;
            var healthRatio = currentHealth / baseHealth;
            var evaluation =
            Math.Abs(effect.Value) * (1 + _healthModifierMultiplier * (1 - healthRatio));
            if (healthRatio <= _criticalHealthRatio)
            {
                evaluation *= _criticalHealthMultiplier;
            }
            finalEvaluation += evaluation;
        }
        return finalEvaluation;
    }
    private float EvaluateStatModifiers(
        List<StatModifierEffect> statModifiers,
        Combatant actor,
        Combatant target,
        int turn)
    {
        float finalEvaluation = 0f;
        foreach (var statModifier in statModifiers)
        {
            var evaluation = EvaluateStatModifier(statModifier, actor, target);
            finalEvaluation += evaluation;
        }
        return finalEvaluation;
    }
    private float EvaluateStatModifier(StatModifierEffect statModifier, Combatant actor, Combatant target)
    {
        // TODO: Check attack buff and defense debuff stacking. 
        // Their evaluation is done before which means that remaining duration 
        // will be +1 relative to actual duration, since if we chose this move, that will trigger
        // tick which will do RemainingDuration-- 

        var modifierTarget = statModifier.Target;
        var modifierOther = statModifier.Target == actor ? target : actor;

        var attack = modifierTarget.Stats[StatType.Attack];
        var defense = modifierOther.Stats[StatType.Defense];
        return 1f;
    }

    private Move StrongestMoveBasedOn(StatType statType, Combatant combatant)
    {
        float strongestValue = 0f;
        Move strongestMove = null;
        foreach (var move in combatant.Moves)
        {
            if ((statType == StatType.Attack && move.Category == MoveCategory.Physical) ||
            (statType == StatType.Magic && move.Category == MoveCategory.Magic))
            {
                var currentValue = _calculationService.CalculateHealtModifierAbsoluteBaseValue(move);
                if (currentValue > strongestValue)
                {
                    strongestValue = currentValue;
                    strongestMove = move;
                }
            }
        }
        return strongestMove;
    }
}
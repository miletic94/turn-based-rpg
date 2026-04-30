using System;

// ======================================================
// MOVE SERVICE
// Application layer use-case service
//
// Responsibilities:
// - Validate move ownership
// - Validate resource cost
// - Advance source modifier state
// - Build execution context
// - Execute effect pipeline
// - Spend resources
// - Cleanup expired modifiers
// ======================================================

public class MoveService
{
    private readonly MoveEffectExecutionService _effectExecutionService;
    private readonly IEventBus _eventBus;

    public MoveService(
        MoveEffectExecutionService effectExecutionService,
        IEventBus eventBus)
    {
        _effectExecutionService = effectExecutionService;
        _eventBus = eventBus;
    }

    public void ExecuteMove(
        Character source,
        Character target,
        Move move)
    {
        ValidateMoveOwnership(source, move);

        ValidateResources(source, move);

        ProcessTurnStart(source);

        var context = CreateEffectContext(source, target);

        ExecuteEffects(move, context);

        SpendMoveCost(source, move);

        ProcessTurnEnd(source);

        _eventBus.Publish(new MoveExecutedEvent(source, target, move));
    }

    // ======================================================
    // VALIDATION
    // ======================================================

    private void ValidateMoveOwnership(
        Character source,
        Move move)
    {
        if (!source.HasMove(move))
        {
            throw new Exception(
                $"{source.Name} does not have move {move.Name}");
        }
    }

    private void ValidateResources(
        Character source,
        Move move)
    {
        if (!source.HasEnoughResource(move))
        {
            throw new Exception(
                $"{source.Name} does not have enough resources. " +
                $"Needs: {move.Cost.Amount} {move.Cost.Type}");
        }
    }

    // ======================================================
    // TURN PROCESSING
    // ======================================================

    private void ProcessTurnStart(Character source)
    {
        source.TickModifiers();
    }

    private void ProcessTurnEnd(Character source)
    {
        source.ClearInactiveModifiers();
    }

    // ======================================================
    // CONTEXT
    // ======================================================

    private EffectContext CreateEffectContext(
        Character source,
        Character target)
    {
        return new EffectContext(source, target);
    }

    // ======================================================
    // EFFECT PIPELINE
    // ======================================================

    private void ExecuteEffects(
        Move move,
        EffectContext context)
    {
        foreach (var effect in move.Effects)
        {
            _effectExecutionService.Execute(effect, context);
        }
    }

    // ======================================================
    // COST
    // ======================================================

    private void SpendMoveCost(
        Character source,
        Move move)
    {
        source.ReduceResource(move.Cost);
    }
}
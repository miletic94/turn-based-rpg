using System;
using UnityEngine;

public enum BattlePhase
{
    TurnStart,
    MoveExecution,
    TurnResolution,
    Finished
}

public class BattleService
{
    private readonly BattleContext _battleContext;
    private readonly BattleTurnService _battleTurnService;
    private readonly BattleResolutionService _battleResolutionService;
    private readonly MoveEffectCalculationService _moveEffectCalculationService;
    private readonly MoveExecutionService _moveExecutionService;
    private readonly IMoveProvider _playerProvider;
    private readonly IMoveProvider _enemyProvider;

    public BattleService(BattleContext battleContext,
        BattleTurnService battleTurnService,
        BattleResolutionService battleResolutionService,
        MoveEffectCalculationService moveEffectCalculationService,
        MoveExecutionService moveExecutionService,
        IMoveProvider playerProvider,
        IMoveProvider enemyProvider)
    {
        _battleContext = battleContext;
        _battleTurnService = battleTurnService;
        _battleResolutionService = battleResolutionService;
        _moveEffectCalculationService = moveEffectCalculationService;
        _moveExecutionService = moveExecutionService;
        _playerProvider = playerProvider;
        _enemyProvider = enemyProvider;
    }

    public BattlePhase Phase { get; private set; } = BattlePhase.TurnStart;
    public Combatant Winner { get; private set; }

    public Combatant CurrentActor => _battleTurnService.GetCurrentCombatant(_battleContext);
    public Combatant CurrentTarget => _battleTurnService.GetNextCombatant(_battleContext);
    public async Awaitable<BattleUpdate> Step()
    {
        if (Phase == BattlePhase.Finished)
        {
            // TODO: This is suspicious
            var (player, enemy) = CurrentActor.Role == CombatantRole.Player
                ? (CurrentActor, CurrentTarget)
                : (CurrentTarget, CurrentActor);
            return new BattleFinishedUpdate(player, enemy, Winner);
        }

        if (Phase == BattlePhase.TurnStart)
        {
            var actor = CurrentActor;

            RemoveExpiredModifiers(actor);
            TickModifiers(actor);

            Phase = BattlePhase.MoveExecution;

            return new TurnStartedUpdate(
                CurrentActor,
                CurrentTarget);
        }

        if (Phase == BattlePhase.MoveExecution)
        {
            var actor = CurrentActor;

            var provider =
                actor.Role == CombatantRole.Player
                    ? _playerProvider
                    : _enemyProvider;

            var move =
                await provider.GetMove(actor);

            var moveEffect = _moveEffectCalculationService.Calculate(move, CurrentActor, CurrentTarget);
            var executedMoveEffect = _moveExecutionService.Execute(moveEffect);

            Phase = BattlePhase.TurnResolution;

            return new MoveExecutedUpdate(CurrentActor, CurrentTarget, executedMoveEffect);
        }

        if (Phase == BattlePhase.TurnResolution)
        {
            if (_battleResolutionService.TryGetWinner(_battleContext, out var winner))
            {
                Winner = winner;
                Phase = BattlePhase.Finished;
            }
            else
            {
                _battleTurnService.AdvanceTurn(_battleContext);
                Phase = BattlePhase.TurnStart;
            }
            return new TurnEndedUpdate();
        }

        throw new InvalidOperationException();
    }

    // TODO: This shouldn't be here
    public void RemoveExpiredModifiers(Combatant currentActor)
    {
        currentActor.RemoveExpiredModifiers();
    }
    public void TickModifiers(Combatant currentActor)
    {
        currentActor.TickModifiers();
    }
}
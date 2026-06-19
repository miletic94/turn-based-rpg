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
    private readonly BattleResolutionService _battleResolutionService;
    private readonly MoveEffectCalculationService _moveEffectCalculationService;
    private readonly MoveExecutionService _moveExecutionService;
    private readonly IMoveProvider _playerProvider;
    private readonly IMoveProvider _enemyProvider;

    public BattleService(BattleContext battleContext,
        BattleResolutionService battleResolutionService,
        MoveEffectCalculationService moveEffectCalculationService,
        MoveExecutionService moveExecutionService,
        IMoveProvider playerProvider,
        IMoveProvider enemyProvider)
    {
        _battleContext = battleContext;
        _battleResolutionService = battleResolutionService;
        _moveEffectCalculationService = moveEffectCalculationService;
        _moveExecutionService = moveExecutionService;
        _playerProvider = playerProvider;
        _enemyProvider = enemyProvider;
    }

    public BattlePhase Phase { get; private set; } = BattlePhase.TurnStart;
    public Combatant Winner { get; private set; }

    private Combatant _currentActor => _battleContext.CurrentActor;
    private Combatant _currentTarget => _battleContext.CurrentTarget;
    public async Awaitable<BattleUpdate> Step()
    {
        if (Phase == BattlePhase.Finished)
        {
            // TODO: This is suspicious
            var (player, enemy) = _currentActor.Role == CombatantRole.Player
                ? (_currentActor, _currentTarget)
                : (_currentTarget, _currentActor);
            return new BattleFinishedUpdate(player, enemy, Winner);
        }

        if (Phase == BattlePhase.TurnStart)
        {
            var actor = _currentActor;

            RemoveExpiredModifiers(actor);
            TickModifiers(actor);

            Phase = BattlePhase.MoveExecution;

            return new TurnStartedUpdate(
                _currentActor,
                _currentTarget);
        }

        if (Phase == BattlePhase.MoveExecution)
        {
            var actor = _currentActor;

            var provider =
                actor.Role == CombatantRole.Player
                    ? _playerProvider
                    : _enemyProvider;

            var move =
                await provider.GetMove(actor);

            var moveEffect = _moveEffectCalculationService.Calculate(move, _currentActor, _currentTarget);
            var executedMoveEffect = _moveExecutionService.Execute(moveEffect);

            Phase = BattlePhase.TurnResolution;

            return new MoveExecutedUpdate(_currentActor, _currentTarget, executedMoveEffect);
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
                _battleContext.AdvanceTurn();
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
using System.Collections.Generic;

public enum BattlePhase
{
    NeedMoveSelection,
    ResolvingTurn,
    Finished
}

public class BattleService
{
    private readonly BattleContext _battleData;
    private readonly BattleTurnService _battleTurnService;
    private readonly BattleResolutionService _battleResolutionService;
    private readonly MoveService _moveService;

    public BattleService(BattleContext battleData, BattleTurnService battleTurnService, BattleResolutionService battleResolutionService, MoveService moveService)
    {
        _battleData = battleData;
        _battleTurnService = battleTurnService;
        _battleResolutionService = battleResolutionService;
        _moveService = moveService;
    }

    public BattlePhase Phase { get; private set; } = BattlePhase.NeedMoveSelection;
    public Combatant Winner { get; private set; }

    public Combatant CurrentActor => _battleTurnService.GetCurrentCombatant(_battleData);
    public Combatant CurrentTarget => _battleTurnService.GetNextCombatant(_battleData);

    public void SubmitMove(Move move)
    {
        if (Phase != BattlePhase.NeedMoveSelection) return;
        _moveService.ApplyMove(CurrentActor, CurrentTarget, move);

        Phase = BattlePhase.ResolvingTurn;
    }

    // TODO: This shouldn't be here
    public void RemoveExpiredModifiers(Combatant currentActor)
    {
        _moveService.RemoveExpiredModifiers(currentActor);
    }
    public void TickModifiers(Combatant currentActor)
    {
        _moveService.TickModifiers(currentActor);
    }

    public void Advance()
    {
        if (Phase == BattlePhase.ResolvingTurn)
        {
            if (_battleResolutionService.TryGetWinner(_battleData, out var winner))
            {
                Winner = winner;
                Phase = BattlePhase.Finished;
                return;
            }

            _battleTurnService.AdvanceTurn(_battleData);
            Phase = BattlePhase.NeedMoveSelection;
        }
    }
}
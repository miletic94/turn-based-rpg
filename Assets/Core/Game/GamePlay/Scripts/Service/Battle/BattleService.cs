using System.Collections.Generic;

public enum BattlePhase
{
    NeedMoveSelection,
    ResolvingTurn,
    Finished
}

public class BattleService
{
    private readonly BattleContext _battleContext;
    private readonly BattleTurnService _battleTurnService;
    private readonly BattleResolutionService _battleResolutionService;
    private readonly MoveService _moveService;

    public BattleService(BattleContext battleContext, BattleTurnService battleTurnService, BattleResolutionService battleResolutionService, MoveService moveService)
    {
        _battleContext = battleContext;
        _battleTurnService = battleTurnService;
        _battleResolutionService = battleResolutionService;
        _moveService = moveService;
    }

    public BattlePhase Phase { get; private set; } = BattlePhase.NeedMoveSelection;
    public Combatant Winner { get; private set; }

    public Combatant CurrentActor => _battleTurnService.GetCurrentCombatant(_battleContext);
    public Combatant CurrentTarget => _battleTurnService.GetNextCombatant(_battleContext);

    public void SubmitMove(Move move)
    {
        if (Phase != BattlePhase.NeedMoveSelection) return;
        _moveService.ApplyMove(CurrentActor, CurrentTarget, move);

        Phase = BattlePhase.ResolvingTurn;
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

    public void Advance()
    {
        if (Phase == BattlePhase.ResolvingTurn)
        {
            if (_battleResolutionService.TryGetWinner(_battleContext, out var winner))
            {
                Winner = winner;
                Phase = BattlePhase.Finished;
                return;
            }

            _battleTurnService.AdvanceTurn(_battleContext);
            Phase = BattlePhase.NeedMoveSelection;
        }
    }
}
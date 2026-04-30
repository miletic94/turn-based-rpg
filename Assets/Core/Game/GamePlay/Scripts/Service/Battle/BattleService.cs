using UnityEngine;

public class BattleService
{
    private readonly BattleState _battleState;
    private readonly BattleTurnService _turnService;
    private readonly BattleResolutionService _resolutionService;
    private readonly MoveService _moveService;

    public BattleService(
        BattleState battleState,
        BattleTurnService turnService,
        BattleResolutionService resolutionService,
        MoveService moveService)
    {
        _battleState = battleState;
        _turnService = turnService;
        _resolutionService = resolutionService;
        _moveService = moveService;
    }

    public async Awaitable RunBattle()
    {
        Character winner;

        while (!_resolutionService.TryGetWinner(_battleState, out winner))
        {
            var source = _turnService.GetCurrentCombatant(_battleState);
            var target = _turnService.GetNextCombatant(_battleState);

            var move = await source.MoveSelector.SelectMoveAsync(source, _battleState);

            _moveService.ExecuteMove(source, target, move);

            _turnService.AdvanceTurn(_battleState);
        }

        Debug.Log($"Winner: {winner.Name}");
    }
}
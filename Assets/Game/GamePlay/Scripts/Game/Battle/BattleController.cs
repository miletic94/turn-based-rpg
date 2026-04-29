using UnityEngine;

public class BattleController
{
    private BattleState _battleState;
    private MoveExecutor _moveExecutor;
    private BattleEventBus _bus;

    public BattleController(BattleState battleState, MoveExecutor moveExecutor, BattleEventBus bus)
    {
        _battleState = battleState;
        _moveExecutor = moveExecutor;
        _bus = bus;
    }

    public async Awaitable BattleLoop()
    {
        Character winner;
        while (!_battleState.TryEnd(out winner))
        {
            var sourceCombatant = _battleState.GetSourceCombatant();
            var targetCombatant = _battleState.GetTargetCombatant();
            var move = await sourceCombatant.Input.GetMoveAsync(_battleState);
            var moveExecutedEvents = _moveExecutor.Execute(sourceCombatant, targetCombatant, move);

            // TODO: Instead of events we can use Commands. See TUAT
            // Instead of MoveExecutedEvent we can just use Character data

            foreach (var moveExecutedEvent in moveExecutedEvents)
            {
                _bus.Publish(moveExecutedEvent);
            }
            // Debug.Log($@"[MOVE {_battleState.TurnNumber}]
            // move:  {move.Name}
            // {sourceCombatant}
            // {targetCombatant}
            // ");
            _battleState.NextTurn();
        }
        Debug.Log($"winner: {winner.Name}");
    }
}
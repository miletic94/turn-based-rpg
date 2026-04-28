using UnityEngine;

public class BattleController
{
    private BattleState _battleState;
    private MoveExecutor _moveExecutor;

    public BattleController(BattleState battleState, MoveExecutor moveExecutor)
    {
        _battleState = battleState;
        _moveExecutor = moveExecutor;
    }

    public async Awaitable BattleLoop()
    {
        Character winner;
        while (!_battleState.TryEnd(out winner))
        {
            var sourceCombatant = _battleState.GetSourceCombatant();
            var targetCombatant = _battleState.GetTargetCombatant();
            var move = await sourceCombatant.Input.GetMoveAsync(_battleState);
            _moveExecutor.Execute(sourceCombatant, targetCombatant, move);
            Debug.Log($@"[MOVE {_battleState.TurnNumber}]
            move:  {move.Name}
            {sourceCombatant}
            {targetCombatant}
            ");
            _battleState.NextTurn();
        }
        Debug.Log(winner.Name);
    }
}
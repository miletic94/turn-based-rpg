using UnityEngine;

public class BattleExecutor
{
    private BattleState _battleState;
    private MoveExecutor _moveExecutor;
    private ICommandFactory _commandFactory;

    public BattleExecutor(ICommandFactory commandFactory, BattleState battleState, MoveExecutor moveExecutor)
    {
        _battleState = battleState;
        _moveExecutor = moveExecutor;
        _commandFactory = commandFactory;
    }

    public async Awaitable BattleLoop()
    {
        Character winner;
        while (!_battleState.TryEnd(out winner))
        {
            var sourceCombatant = _battleState.GetSourceCombatant();
            var targetCombatant = _battleState.GetTargetCombatant();
            var move = await sourceCombatant.MoveProvider.GetMoveAsync(_battleState);
            _moveExecutor.Execute(sourceCombatant, targetCombatant, move);

            _commandFactory.CreateCommandVoid<MoveExecutedCommand>()
            .SetData(new MoveExecutedCommandData(
                _battleState.GetPlayer(), _battleState.GetEnemy()))
                .Execute();

            _battleState.NextTurn();
        }
        Debug.Log($"winner: {winner.Name}");
    }
}
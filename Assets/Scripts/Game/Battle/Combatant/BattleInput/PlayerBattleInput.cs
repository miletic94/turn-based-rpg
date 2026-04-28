using UnityEngine;

public class PlayerBattleInput : IBattleInput
{
    private readonly IMoveView _moveView;
    public PlayerBattleInput(IMoveView moveView)
    {
        _moveView = moveView;
    }
    public Awaitable<Move> GetMoveAsync(BattleState state)
    {
        var currentMove = new AwaitableCompletionSource<Move>();

        _moveView.EnableInput(move =>
        {
            currentMove.SetResult(move);
        });

        return currentMove.Awaitable;
    }
}
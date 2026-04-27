using UnityEngine;

public class PlayerBattleInput : IBattleInput
{
    private readonly IBattleView _ui;
    public Awaitable<Move> GetMoveAsync(BattleState state)
    {
        var currentMove = new AwaitableCompletionSource<Move>();

        _ui.ShowMoveSelection(state, move =>
        {
            currentMove.SetResult(move);
        });

        return currentMove.Awaitable;
    }
}
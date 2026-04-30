using UnityEngine;

public class PlayerBattleMoveProvider : IBattleMoveProvider
{
    private readonly MoveController _moveController;

    public PlayerBattleMoveProvider(MoveController moveController)
    {
        _moveController = moveController;
    }

    public Awaitable<Move> GetMoveAsync(BattleState state)
    {
        var character = state.GetPlayer();
        return _moveController.WaitForPlayerMove(character);
    }
}
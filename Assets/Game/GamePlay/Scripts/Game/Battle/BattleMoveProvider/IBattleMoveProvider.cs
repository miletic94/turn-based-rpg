using UnityEngine;

public interface IBattleMoveProvider
{
    Awaitable<Move> GetMoveAsync(BattleState state);
}
using UnityEngine;

public interface IBattleInput
{
    Awaitable<Move> GetMoveAsync(BattleState state);
}
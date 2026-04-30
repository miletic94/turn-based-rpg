using UnityEngine;

public interface IBattleMoveSelector
{
    Awaitable<Move> SelectMoveAsync(Character actor, BattleState state);
}
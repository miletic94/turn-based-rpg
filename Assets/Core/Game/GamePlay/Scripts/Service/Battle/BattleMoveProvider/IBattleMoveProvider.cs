using UnityEngine;

public interface ICombatMoveSelector
{
    Awaitable<Move> SelectMoveAsync(Character actor, BattleState state);
}
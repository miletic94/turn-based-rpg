using UnityEngine;

public interface IBattleMoveSelector
{
    Awaitable<Move> SelectMoveAsync(Combatant actor, BattleData state);
}
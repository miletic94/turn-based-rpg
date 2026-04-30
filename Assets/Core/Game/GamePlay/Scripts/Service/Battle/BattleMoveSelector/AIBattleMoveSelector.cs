using UnityEngine;

public class AIBattleMoveSelector : IBattleMoveSelector
{
    public async Awaitable<Move> SelectMoveAsync(
           Character actor,
           BattleData battleState)
    {
        await Awaitable.WaitForSecondsAsync(0.1f);

        var moves = actor.Moves;

        return moves[Random.Range(0, moves.Count)];
    }
}
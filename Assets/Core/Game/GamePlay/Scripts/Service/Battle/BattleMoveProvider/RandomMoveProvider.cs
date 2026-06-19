using UnityEngine;

public class RnadomMoveProvider : IMoveProvider
{
    public async Awaitable<Move> GetMove(
           BattleContext context)
    {
        var moves = context.CurrentActor.Moves;
        return moves[Random.Range(0, moves.Count)];
    }
}
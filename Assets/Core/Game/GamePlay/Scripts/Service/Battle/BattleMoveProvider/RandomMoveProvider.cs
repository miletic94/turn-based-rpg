using UnityEngine;

public class RnadomMoveProvider : IMoveProvider
{
    public async Awaitable<Move> GetMove(
           Combatant actor)
    {
        await Awaitable.WaitForSecondsAsync(1f);

        var moves = actor.Moves;
        var move = moves[moveQueue[moveIndex]];
        moveIndex = (moveIndex + 1) % moveQueue.Length;
        return move;
        // return moves[Random.Range(0, moves.Count)];
    }
}
using UnityEngine;

public class AIBattleMoveSelector : IMoveProvider
{
    private int[] moveQueue = new int[] { 1, 1, 1, 2, 2, 2 };
    private int moveIndex = 0;
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
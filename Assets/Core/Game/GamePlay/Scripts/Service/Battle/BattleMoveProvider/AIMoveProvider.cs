using UnityEngine;

public class AIBattleMoveSelector : IMoveProvider
{
    private int[] moveQueue = new int[] { 2, 0, 0 };
    private int moveIndex = 0;
    public async Awaitable<Move> GetMove(
           Combatant actor)
    {
        await Awaitable.WaitForSecondsAsync(0.5f);

        var moves = actor.Moves;
        var move = moves[moveQueue[moveIndex]];
        moveIndex = (moveIndex + 1) % moveQueue.Length;
        return move;
        // return moves[Random.Range(0, moves.Count)];
    }
}
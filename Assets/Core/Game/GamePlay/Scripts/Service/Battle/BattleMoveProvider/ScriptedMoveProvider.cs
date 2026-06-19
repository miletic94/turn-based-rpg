using UnityEngine;

public class ScriptedMoveProvider : IMoveProvider
{
    private int[] moveQueue = new int[] { 2, 2, 2 };
    private int moveIndex = 0;
    public async Awaitable<Move> GetMove(
           BattleContext context)
    {
        await Awaitable.WaitForSecondsAsync(1f);

        var moves = context.CurrentActor.Moves;
        var move = moves[moveQueue[moveIndex]];
        moveIndex = (moveIndex + 1) % moveQueue.Length;
        return move;
    }
}
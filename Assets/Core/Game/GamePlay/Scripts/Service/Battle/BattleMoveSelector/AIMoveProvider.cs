using UnityEngine;

public class AIBattleMoveSelector : IMoveProvider
{
    public async Awaitable<Move> GetMove(
           Combatant actor)
    {
        await Awaitable.WaitForSecondsAsync(0.1f);

        var moves = actor.Moves;

        return moves[Random.Range(0, moves.Count)];
    }
    public void OnMoveSelected(Move move)
    {
        throw new System.NotImplementedException();
    }
}
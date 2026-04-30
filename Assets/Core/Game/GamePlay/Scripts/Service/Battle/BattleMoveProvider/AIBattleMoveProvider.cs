using UnityEngine;

public class AICombatMoveSelector : ICombatMoveSelector
{
    public async Awaitable<Move> SelectMoveAsync(
           Character actor,
           BattleState battleState)
    {
        await Awaitable.WaitForSecondsAsync(0.1f);

        var moves = actor.Moves;

        return moves[Random.Range(0, moves.Count)];
    }
}
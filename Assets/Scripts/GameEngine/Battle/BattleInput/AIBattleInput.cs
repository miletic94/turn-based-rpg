using UnityEngine;

public class AIBattleInput : IBattleInput
{
    public async Awaitable<Move> GetMoveAsync(BattleState state)
    {
        var moves = state.GetSourceCombatant().Moves;

        await Awaitable.WaitForSecondsAsync(0.1f);

        return moves[Random.Range(0, moves.Count)];
    }
}
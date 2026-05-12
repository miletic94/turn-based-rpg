using UnityEngine;

public interface IMoveProvider
{
    Awaitable<Move> GetMove(Combatant actor);
    public void OnMoveSelected(Move move);
}
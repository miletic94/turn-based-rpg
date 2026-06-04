using UnityEngine;

public interface IMoveProvider
{
    Awaitable<Move> GetMove(Combatant actor);
}
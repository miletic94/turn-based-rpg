using UnityEngine;

public interface IMoveProvider
{
    Awaitable<Move> GetMove(BattleContext context);
}
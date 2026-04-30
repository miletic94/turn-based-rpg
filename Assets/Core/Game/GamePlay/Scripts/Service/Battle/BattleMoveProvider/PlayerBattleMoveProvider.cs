using UnityEngine;

public class PlayerCombatMoveSelector : ICombatMoveSelector
{
    private readonly MoveController _moveController;

    public PlayerCombatMoveSelector(MoveController moveController)
    {
        _moveController = moveController;
    }

    public Awaitable<Move> SelectMoveAsync(
        Character actor,
        BattleState battleState)
    {
        return _moveController.WaitForPlayerMove(actor);
    }
}
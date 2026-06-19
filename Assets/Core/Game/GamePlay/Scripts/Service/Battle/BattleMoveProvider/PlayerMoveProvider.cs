using UnityEngine;

public class PlayerMoveProvider : IMoveProvider
{
    private MoveSelectionService _moveSelectionService;
    public PlayerMoveProvider(MoveSelectionService moveSelectionSErvice)
    {
        _moveSelectionService = moveSelectionSErvice;
    }
    public Awaitable<Move> GetMove(BattleContext context)
    {
        return _moveSelectionService.RequestMove();
    }
}
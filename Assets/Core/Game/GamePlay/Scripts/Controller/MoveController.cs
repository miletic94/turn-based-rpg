using System;
using System.Collections.Generic;
public class MoveController
{
    private readonly MoveView _moveView;
    private readonly MoveDescriptionService _moveDescriptionService;
    public MoveController(MoveView moveView, MoveDescriptionService moveDescriptionService)
    {
        _moveView = moveView;
        _moveDescriptionService = moveDescriptionService;
    }

    public void Initialize(List<Move> moves, Action<Move> onMoveSelected)
    {
        _moveView.ShowMoves(moves, onMoveSelected, OnMoveHovered);
    }

    public void OnMoveHovered(Move move)
    {
        UnityEngine.Debug.Log(_moveDescriptionService.Describe(move));
    }
}
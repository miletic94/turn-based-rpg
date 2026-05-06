using System;
using System.Collections.Generic;
public class MoveController
{
    private readonly MoveView _moveView;
    public MoveController(MoveView moveView)
    {
        _moveView = moveView;
    }

    public void Initialize(List<Move> moves, Action<Move> onMoveSelected)
    {
        _moveView.ShowMoves(moves, onMoveSelected);
    }
}
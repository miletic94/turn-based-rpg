using System;
using System.Collections.Generic;

public class BattleMoveController
{
    private readonly BattleMoveView _battleMoveView;
    private readonly MoveTooltipBinder _moveTooltipBinder;

    public BattleMoveController(BattleMoveView battleView, MoveTooltipBinder moveTooltipBinder)
    {
        _battleMoveView = battleView;
        _moveTooltipBinder = moveTooltipBinder;
    }

    public void ShowMoves(
        List<Move> moves,
        Action<Move> onSelected)
    {
        _battleMoveView.ClearMoves();

        foreach (Move move in moves)
        {
            MoveView view =
                _battleMoveView.AddMove();

            Bind(move, view, onSelected);
        }
    }

    private void Bind(
        Move move,
        MoveView view,
        Action<Move> onSelected)
    {
        view.
            SetLabel(move.Name).
            MakeClickable(() => onSelected(move));

        _moveTooltipBinder.BindTooltip(move, view);
    }
}
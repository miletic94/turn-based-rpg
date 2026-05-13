using System;
using System.Collections.Generic;

public interface IMoveView
{
    void ShowMoves(List<Move> moves, Action<Move> onMoveSelected, Action<Move> onMoveHovered);
}
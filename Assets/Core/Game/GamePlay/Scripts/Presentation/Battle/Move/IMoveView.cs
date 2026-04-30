using System;
using System.Collections.Generic;

public interface IMoveView
{
    void ShowMoves(List<Move> moves);
    void EnableInput(Action<Move> onMoveSelected);
    void DisableInput();
    void OnMoveButtonClicked(Move move);
}
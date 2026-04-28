using System;
using System.Collections.Generic;

public interface IMoveView
{
    void SetMoves(List<Move> moves);
    void EnableInput(Action<Move> onMoveSelected);
    void DisableInput();
}
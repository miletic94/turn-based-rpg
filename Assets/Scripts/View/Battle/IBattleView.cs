using System;

public interface IBattleView
{
    void ShowMoveSelection(BattleState state, Action<Move> onMoveSelected);
}
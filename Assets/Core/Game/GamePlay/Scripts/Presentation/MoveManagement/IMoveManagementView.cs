using System;
using System.Collections.Generic;

public interface IMoveManagementView
{
    void Show(
        List<Move> availableMoves,
        List<Move> equippedMoves);

    void BindDropZones(
        Action<MoveItemView, MoveDropZone.ZoneType> onDropped);

    void BindSave(Action onSave);

    void Unbind();
}
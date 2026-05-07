using System;
using System.Collections.Generic;

public interface IMoveManagementView
{
    event Action<Move, MoveDropZone.ZoneType> MoveDropped;
    event Action SaveClicked;

    void Render(
        List<Move> available,
        List<Move> equipped);

    void ResetDrag(Move move);
}
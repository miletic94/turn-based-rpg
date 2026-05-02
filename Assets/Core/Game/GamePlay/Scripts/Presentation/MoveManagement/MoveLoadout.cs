using System.Collections.Generic;

public class MoveLoadout
{
    public List<Move> AvailableMoves = new();
    public List<Move> EquippedMoves = new();

    public int MaxEquipped = 4;
}
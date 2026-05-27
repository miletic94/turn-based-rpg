using System.Collections.Generic;

public class MoveLoadout
{
    public HashSet<int> AvailableMoves = new();
    public HashSet<int> EquippedMoves = new();

    public int MaxEquipped = 4;
}
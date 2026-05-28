using System.Collections.Generic;

public class MoveLoadout
{
    public HashSet<int> AvailableMoves = new();
    public HashSet<int> EquippedMoves = new();

    // TODO: Here MaxEquiped and MinEquipped are hardcoded. 
    // They should be part of game rules domain
    public int MaxEquipped = 4;
    public int MinEquipped = 4;
}
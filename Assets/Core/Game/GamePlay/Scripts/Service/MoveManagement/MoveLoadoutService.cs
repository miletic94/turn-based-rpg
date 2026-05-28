using System.Collections.Generic;
public class MoveLoadoutService
{
    private readonly MoveLoadout _loadout;
    public MoveLoadoutService(
        MoveLoadout loadout)
    {
        _loadout = loadout;
    }

    public bool TryEquip(int moveId)
    {
        if (_loadout.EquippedMoves.Contains(moveId))
            return true;

        if (!_loadout.AvailableMoves.Remove(moveId))
            return false;

        if (_loadout.EquippedMoves.Count >= _loadout.MaxEquipped)
            return false;

        _loadout.EquippedMoves.Add(moveId);
        return true;
    }

    public bool TryUnequip(int moveId)
    {
        if (_loadout.AvailableMoves.Contains(moveId))
            return true;

        if (!_loadout.EquippedMoves.Remove(moveId))
            return false;

        _loadout.AvailableMoves.Add(moveId);
        return true;
    }
}
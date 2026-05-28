using System.Collections.Generic;
using System.Linq;
public class MoveLoadoutService
{
    private readonly MoveLoadout _loadout;
    public List<int> AvailableMoves => _loadout.AvailableMoves.ToList();
    public List<int> EquippedMoves => _loadout.EquippedMoves.ToList();


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

    public void UnequipAll()
    {
        _loadout.AvailableMoves.UnionWith(_loadout.EquippedMoves);
        _loadout.EquippedMoves.Clear();
    }

    public bool TrySave(out string errorMessage)
    {
        errorMessage = null;
        if (_loadout.EquippedMoves.Count < _loadout.MinEquipped)
        {
            errorMessage = $"Equip {_loadout.MinEquipped} moves first";
            return false;
        }
        return true;
    }
}
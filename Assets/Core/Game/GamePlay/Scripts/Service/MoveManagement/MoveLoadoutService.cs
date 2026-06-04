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

    public bool CanEquip(int moveId)
    {
        if (_loadout.EquippedMoves.Contains(moveId))
            return true;
        if (!_loadout.AvailableMoves.Contains(moveId))
            return false;
        if (_loadout.EquippedMoves.Count >= _loadout.MaxEquipped)
            return false;

        return true;
    }

    public void Equip(int moveId)
    {
        _loadout.AvailableMoves.Remove(moveId);
        _loadout.EquippedMoves.Add(moveId);
    }

    public bool CanUnequip(int moveId)
    {
        if (_loadout.AvailableMoves.Contains(moveId))
            return true;

        if (!_loadout.EquippedMoves.Contains(moveId))
            return false;

        return true;
    }

    public void Unequip(int moveId)
    {
        _loadout.EquippedMoves.Remove(moveId);
        _loadout.AvailableMoves.Add(moveId);
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
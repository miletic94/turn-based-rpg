public class MoveLoadoutService
{
    private readonly MoveLoadout _loadout;

    public MoveLoadoutService(
        MoveLoadout loadout)
    {
        _loadout = loadout;
    }

    public bool MoveToEquipped(Move move)
    {
        if (_loadout.EquippedMoves.Count >=
            _loadout.MaxEquipped)
            return false;

        if (!_loadout.AvailableMoves.Remove(move))
            return false;

        _loadout.EquippedMoves.Add(move);

        return true;
    }

    public bool MoveToAvailable(Move move)
    {
        if (!_loadout.EquippedMoves.Remove(move))
            return false;

        _loadout.AvailableMoves.Add(move);

        return true;
    }
}
using System.Collections.Generic;

public class MoveDescriptionService
{
    private readonly Dictionary<int, Move> _moveById; // TODO: This should be database
    public MoveDescriptionService(Dictionary<int, Move> moveById)
    {
        _moveById = moveById;
    }
    public string Describe(int moveId)
    {
        var move = _moveById.TryGetValue(moveId, out var m) ? m : null;
        if (move == null)
        {
            throw new System.Exception("Move not found.");
        }
        var effects = move.Effects;
        string result = "";
        foreach (var e in effects)
        {
            result += DescribeEffect(e);
            result += ".\n";
        }
        return result;
    }
    public string DescribeEffect(IMoveEffect effect)
    {

        return $"{effect}";
    }
}
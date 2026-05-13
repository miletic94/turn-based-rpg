public class MoveDescriptionService
{
    public string Describe(Move move)
    {
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
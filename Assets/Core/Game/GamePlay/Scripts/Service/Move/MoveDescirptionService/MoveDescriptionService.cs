using System.Linq;

public class MoveDescriptionService
{
    public string Describe(Move move)
    {
        string description = "";
        var effects = move.HealthModifiers.Concat<IDescribable>(move.StatModifiers).ToArray();
        for (int i = 0; i < effects.Length; i++)
        {
            description += effects[i].Description;
            if (i + 1 != effects.Length) description += " and ";
            else description += ".";
        }
        if (move.Category == MoveCategory.Physical)
        {
            description += " Scales off attack, reduced by defense";
        }
        if (move.Category == MoveCategory.Magic)
        {
            description += " Scales off magic";
        }
        return description;
    }
}
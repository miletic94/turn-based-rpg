using System.Linq;

public static class MoveFactory
{
    public static Move Create(MoveDTO dto)
    {
        return new Move(
            dto.Id,
            dto.Name,
            dto.Cost,
            dto.Effects.Select(EffectFactory.Create).ToList()
        );
    }
}
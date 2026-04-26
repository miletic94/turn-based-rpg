using System.Linq;
public static class CharacterFactory
{
    public static Character Create(CharacterDTO dto)
    {
        return new Character(
            dto.Name,
            dto.Health,
            dto.Attack,
            dto.Defense,
            dto.Magic,
            (dto.Moves ?? Enumerable.Empty<MoveDTO>())
                .Select(MoveFactory.Create)
                .ToList()
        );
    }
}
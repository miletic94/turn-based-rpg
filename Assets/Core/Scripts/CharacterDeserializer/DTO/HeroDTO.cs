using System.Collections.Generic;

public class HeroDTO
{
    public string Name;
    public float Health;
    public float Attack;
    public float Defense;
    public float Magic;
    public XP xp;
    public int AvailableStatPoints;
    public List<MoveDTO> AvailableMoves;
    public List<MoveDTO> EquippedMoves;
    public Hero ToHero() => new Hero
    (
         Name,
         Health,
         Attack,
         Defense,
         Magic,
         xp,
         AvailableStatPoints,
         AvailableMoves.ConvertAll(m => m.ToMove()),
        EquippedMoves.ConvertAll(m => m.ToMove())
    );
    public CharacterDTO ToCharacter() => new CharacterDTO
    {
        Name = Name,
        Health = Health,
        Attack = Attack,
        Defense = Defense,
        Magic = Magic,
        Moves = EquippedMoves
    };
}

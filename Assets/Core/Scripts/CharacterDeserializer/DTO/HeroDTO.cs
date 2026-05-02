using System.Collections.Generic;

public class HeroDTO
{
    public string Name;
    public float Health;
    public float Attack;
    public float Defense;
    public float Magic;
    public XP xp;
    public List<MoveDTO> AvailableMoves;
    public List<MoveDTO> EquippedMoves;
    public Hero ToHero() => new Hero
    {
        Name = Name,
        Health = Health,
        Attack = Attack,
        Defense = Defense,
        Magic = Magic,
        xp = xp,
        AvailableMoves = AvailableMoves.ConvertAll(m => m.ToMove()),
        EquippedMoves = EquippedMoves.ConvertAll(m => m.ToMove())
    };
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

public class Hero
{
    public string Name;
    public float Health;
    public float Attack;
    public float Defense;
    public float Magic;
    public XP xp;
    public List<Move> AvailableMoves;
    public List<Move> EquippedMoves;

    public Character ToCharacter() => new Character
    {
        Name = Name,
        Health = Health,
        Attack = Attack,
        Defense = Defense,
        Magic = Magic,
        Moves = EquippedMoves
    };
}
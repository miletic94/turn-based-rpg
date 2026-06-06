using System.Collections.Generic;

public class CharacterDTO
{
    public int Id;
    public string Name;
    public float Health;
    public float Attack;
    public float Defense;
    public float Magic;
    public string SpriteAddress;
    public List<MoveDTO> Moves;

    public Character ToCharacter() => new Character
    {
        Id = Id,
        Name = Name,
        Health = Health,
        Attack = Attack,
        Defense = Defense,
        Magic = Magic,
        SpriteAddress = SpriteAddress,
        Moves = Moves.ConvertAll(m => m.ToMove())
    };
}
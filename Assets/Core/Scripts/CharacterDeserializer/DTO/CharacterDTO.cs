using System.Collections.Generic;

public class CharacterDTO
{
    public string Name;
    public float Health;
    public float Attack;
    public float Defense;
    public float Magic;
    public List<MoveDTO> Moves;

    public Character ToCharacter() => new Character
    {
        Name = Name,
        Health = Health,
        Attack = Attack,
        Defense = Defense,
        Magic = Magic,
        Moves = Moves.ConvertAll(m => m.ToMove())
    };
}
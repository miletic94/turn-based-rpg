using System.Collections.Generic;

public class HeroDTO
{
    public string Name;
    public float Health;
    public float Attack;
    public float Defense;
    public float Magic;
    public string SpriteAddress;
    public XpDTO Xp;
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
         SpriteAddress,
         Xp.ToXp(),
         AvailableStatPoints,
         AvailableMoves.ConvertAll(m => m.ToMove()),
        EquippedMoves.ConvertAll(m => m.ToMove())
    );
}

using System.Collections.Generic;

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

    public Combatant ToCombatant() => new Combatant
        (
            Name,
            Health,
            Attack,
            Defense,
            Magic,
            EquippedMoves
        );
}
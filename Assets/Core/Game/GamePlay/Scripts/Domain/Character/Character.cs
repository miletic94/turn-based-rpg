using System.Collections.Generic;

public class Character
{
    public string Name;
    public float Health;
    public float Attack;
    public float Defense;
    public float Magic;
    public List<Move> Moves;
    public Combatant ToCombatant() => new Combatant
    (
        Name,
        Health,
        new CombatantStats(Attack, Defense, Magic),
        Moves
    );
}
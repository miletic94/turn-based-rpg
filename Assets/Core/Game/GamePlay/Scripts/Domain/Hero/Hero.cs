using System.Collections.Generic;

public class Hero
{
    public string Name { get; private set; }
    public float Health { get; private set; }
    public float Attack { get; private set; }
    public float Defense { get; private set; }
    public float Magic { get; private set; }
    public Xp Xp { get; private set; }
    public int AvailableStatPoints { get; private set; }
    public List<Move> AvailableMoves { get; private set; }
    public List<Move> EquippedMoves { get; private set; }
    public Hero(string name, float health, float attack, float defense, float magic, Xp xp, int availableStatPoints, List<Move> availableMoves, List<Move> equippedMoves)
    {
        Name = name;
        Health = health;
        Attack = attack;
        Defense = defense;
        Magic = magic;
        Xp = xp;
        AvailableStatPoints = availableStatPoints;
        AvailableMoves = availableMoves;
        EquippedMoves = equippedMoves;
    }
    public Combatant ToCombatant() => new Combatant
        (
            Name,
            Health,
            Attack,
            Defense,
            Magic,
            EquippedMoves
        );
    public void SetAvailableMoves(List<Move> availableMoves)
    {
        AvailableMoves = availableMoves;
    }
    public void SetEquippedMoves(List<Move> equippedMoves)
    {
        EquippedMoves = equippedMoves;
    }
    public void SetStat(StatType statType, float value)
    {
        switch (statType)
        {
            case StatType.Attack:
                Attack = value;
                break;
            case StatType.Defense:
                Defense = value;
                break;
            case StatType.Magic:
                Magic = value;
                break;
        }
    }
    public void SetAvaialbleStatPoints(int value)
    {
        AvailableStatPoints = value;
    }
}
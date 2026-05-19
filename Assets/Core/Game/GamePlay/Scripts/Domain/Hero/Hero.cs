using System.Collections.Generic;

public class Hero
{
    public string Name { get; private set; }
    public float Health { get; private set; }
    // TODO: Hero shouldn't depend on Stats data structure
    public Stats Stats { get; private set; }
    public Xp Xp { get; private set; }
    public int AvailableStatPoints { get; private set; }
    public List<Move> AvailableMoves { get; private set; }
    public List<Move> EquippedMoves { get; private set; }
    public Hero(string name, float health, Stats stats, Xp xp, int availableStatPoints, List<Move> availableMoves, List<Move> equippedMoves)
    {
        Name = name;
        Health = health;
        Stats = stats;
        Xp = xp;
        AvailableStatPoints = availableStatPoints;
        AvailableMoves = availableMoves;
        EquippedMoves = equippedMoves;
    }
    public Combatant ToCombatant() => new Combatant
        (
            Name,
            Health,
            new CombatantStats(Stats.GetStat(StatType.Attack).CurrentValue,
                Stats.GetStat(StatType.Defense).CurrentValue,
                Stats.GetStat(StatType.Magic).CurrentValue),
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
        Stats.SetStatValue(statType, value);
    }
    public void SetAvaialbleStatPoints(int value)
    {
        AvailableStatPoints = value;
    }
    public void AddAvailableStatPoints(int amount)
    {
        AvailableStatPoints += amount;
    }
}
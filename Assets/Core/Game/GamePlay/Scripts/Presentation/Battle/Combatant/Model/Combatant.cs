using System.Collections.Generic;
using UnityEngine;

public class Combatant
{
    public string Name { get; private set; }
    private float _baseHealth;
    public float BaseHealth => _baseHealth;
    // TODO: Health should be integer
    public float Health { get; private set; }
    public CombatantStats Stats { get; private set; }
    public Sprite Portrait { get; private set; }
    public int Mana { get; private set; } = 4;
    public List<Move> Moves { get; private set; }
    public CombatantRole Role { get; set; }
    public Combatant(string name, float health, CombatantStats stats, Sprite portrait, List<Move> moves)
    {
        Name = name;
        Health = health;
        _baseHealth = health;
        Stats = stats;
        Portrait = portrait;
        Moves = moves;
    }

    public void SetHealth(float value)
    {
        Health = value;
    }
    public void AddActiveModifier(ActiveModifier modifier)
    {
        Stats.AddActiveModifier(modifier);
    }
    public void RemoveExpiredModifiers()
    {
        Stats.RemoveExpiredModifiers();
    }

    public void TickModifiers()
    {
        Stats.TickModifiers();
    }

    // TODO: This is debug helper. Not for final build
    public override string ToString()
    {
        return $@"[{Name}]
        health: {Health}
        attack: {Stats.GetStat(StatType.Attack)} - modifiers: {Stats.GetActiveModifiersToString(StatType.Attack)},
        defense: {Stats.GetStat(StatType.Defense)} - modifiers: {Stats.GetActiveModifiersToString(StatType.Defense)}
        magic: {Stats.GetStat(StatType.Magic)} - modifiers: {Stats.GetActiveModifiersToString(StatType.Magic)}
        ";
    }
}

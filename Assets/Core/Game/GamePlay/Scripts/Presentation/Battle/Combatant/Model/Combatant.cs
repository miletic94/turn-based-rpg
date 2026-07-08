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

    // TODO: Remove the next two methods
    public void RemoveExpiredModifiers()
    {
        Stats.RemoveExpiredModifiers();
    }

    public void TickModifiers()
    {
        Stats.TickModifiers();
    }
}

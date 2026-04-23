using System;
using System.Collections.Generic;
using System.Linq;

public class Character
{
    public string Name { get; private set; }
    private int _baseHealth;
    public int Health { get; private set; }
    private int _baseAttack;
    public int Attack => GetStat(StatType.Attack);
    private int _baseDefense;
    public int Defense => GetStat(StatType.Defense);
    private int _baseMagic;
    public int Magic => GetStat(StatType.Magic);
    public Move[] Moves { get; private set; }
    private List<ActiveModifier> _modifiers;
    public Character(string name, int health, int attack, int defense, int magic, Move[] moves)
    {
        Name = name;
        Health = health;
        _baseHealth = health;
        _baseAttack = attack;
        _baseDefense = defense;
        _baseMagic = magic;
        Moves = moves;
    }

    public void ApplyDamage(int damage)
    {
        Health = Math.Max(0, Health - damage);
    }

    public void Heal(int amount)
    {
        Health = Math.Min(Health + amount, _baseHealth);
    }

    public void ApplyModifier(ModifierType type, StatType stat, int value, int duration)
    {
        _modifiers.Add(new ActiveModifier(type, stat, value, duration));
    }

    public int GetStat(StatType statType)
    {
        int baseValue = statType switch
        {
            StatType.Attack => _baseAttack,
            StatType.Defense => _baseDefense,
            StatType.Magic => _baseMagic,
            _ => 0
        };
        int modifierSum =
        _modifiers
            .Where(m => m.Stat == statType)
            .Sum(m => m.Value);

        return baseValue + modifierSum;
    }
}

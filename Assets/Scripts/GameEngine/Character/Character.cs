using System;
using System.Collections.Generic;
using System.Linq;

public class Character
{
    public string Name { get; private set; }
    private float _baseHealth;
    public float Health { get; private set; }
    private float _baseAttack;
    public float Attack => GetStat(StatType.Attack);
    private float _baseDefense;
    public float Defense => GetStat(StatType.Defense);
    private float _baseMagic;
    public float Magic => GetStat(StatType.Magic);
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

    public void ApplyDamage(float damage)
    {
        Health = Math.Max(0, Health - damage);
    }

    public void Heal(float amount)
    {
        Health = Math.Min(Health + amount, _baseHealth);
    }

    public void ApplyModifier(ModifierType type, StatType stat, float value, ModifierTickBehavior tickBehavior, int duration)
    {
        _modifiers.Add(new ActiveModifier(type, stat, value, tickBehavior, duration));
    }

    public float GetStat(StatType statType)
    {
        float baseValue = statType switch
        {
            StatType.Attack => _baseAttack,
            StatType.Defense => _baseDefense,
            StatType.Magic => _baseMagic,
            _ => 0
        };
        float modifierSum =
        _modifiers
            .Where(m => m.Stat == statType)
            .Sum(m => m.Value);

        return baseValue + modifierSum;
    }
}

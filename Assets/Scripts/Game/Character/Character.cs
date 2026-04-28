using System;
using System.Collections.Generic;
using System.Linq;

public class Character
{
    public string Name { get; private set; }
    private float _baseHealth;
    public float Health { get; private set; }
    private float _baseAttack;
    // TODO: Maybe make a dictionary out of stat getters, so we can remove switch from GetStat as part of making stats extensible
    public float Attack => GetStat(StatType.Attack);
    private float _baseDefense;
    public float Defense => GetStat(StatType.Defense);
    private float _baseMagic;
    public float Magic => GetStat(StatType.Magic);
    public int Mana { get; private set; }
    public List<Move> Moves { get; private set; }
    private List<ActiveModifier> _modifiers;
    public CombatantRole Role { get; set; }
    public IBattleInput Input { get; set; }
    public Character(string name, float health, float attack, float defense, float magic, int mana, List<Move> moves)
    {
        Name = name;
        Health = health;
        _baseHealth = health;
        _baseAttack = attack;
        _baseDefense = defense;
        _baseMagic = magic;
        Mana = mana;
        Moves = moves;
        _modifiers = new List<ActiveModifier>();
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

    public void TickModifiers()
    {
        foreach (var m in _modifiers)
        {
            m.Tick();
        }
    }

    public void ClearInactiveModifiers()
    {
        _modifiers.RemoveAll(m => m.RemainingDuration <= 0);
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
            .Sum(m =>
            {
                return m.Value * (m.TickBehavior == ModifierTickBehavior.Once ? 1 : m.DurationDelta);
            });

        return baseValue + modifierSum;
    }

    public bool HasMove(Move move)
    {
        if (Moves.Find(m => m.Id == move.Id) == null) return false;
        return true;
    }

    public bool HasEnoughResource(Move move)
    {
        var costType = move.Cost.Type;
        var resource = costType switch
        {
            ResourceType.Mana => Mana,
            ResourceType.Health => Health,
            ResourceType.None => float.PositiveInfinity,
            _ => throw new Exception($"Unkonw resource type {costType}")
        };

        if (resource < move.Cost.Amount) return false;
        return true;
    }

    public void ReduceResource(Cost cost)
    {
        switch (cost.Type)
        {
            case ResourceType.Mana:
                Mana -= cost.Amount;
                break;
            case ResourceType.Health:
                Health -= cost.Amount;
                break;
        }
    }


    // TODO: This is debug helper. Not for final build
    public override string ToString()
    {
        return $@"[{Name}]
        health: {Health}
        attack: {Attack},
        defense: {Defense},
        magic: {Magic}
        ";
    }
}

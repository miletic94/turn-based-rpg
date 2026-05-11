using System;
using System.Collections.Generic;
using System.Linq;

public class Combatant
{
    public string Name { get; private set; }
    private float _baseHealth;
    public float BaseHealth => _baseHealth;
    // TODO: Health should be integer
    public float Health { get; private set; }
    public Stats Stats { get; private set; }
    public int Mana { get; private set; } = 4;
    public List<Move> Moves { get; private set; }
    private List<ActiveModifier> _modifiers;
    public CombatantRole Role { get; set; }
    public Combatant(string name, float health, Stats stats, List<Move> moves)
    {
        Name = name;
        Health = health;
        _baseHealth = health;
        Stats = stats;
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

    public void ApplyModifier(ModifierType type, StatType statType, float value, ModifierTickBehavior tickBehavior, int duration)
    {
        var baseStat = Stats.GetStat(statType).CurrentValue;
        var modifier = value * (type == ModifierType.Debuff ? -1 : 1);
        Stats.SetStatValue(statType, baseStat + modifier);

        _modifiers.Add(new ActiveModifier(type, statType, value, tickBehavior, duration));
    }

    public void UnapplyModifier(ActiveModifier modifier)
    {
        var currentValue = Stats.GetStat(modifier.Stat).CurrentValue;
        var modifierValue = modifier.Value
            * (modifier.Type == ModifierType.Debuff ? 1 : -1);

        Stats.SetStatValue(modifier.Stat, currentValue + modifierValue);
    }

    public void ClearInactiveModifiers()
    {
        var expired = _modifiers
            .Where(m => m.RemainingDuration <= 0)
            .ToList();

        expired.ForEach(m => UnapplyModifier(m));

        _modifiers.RemoveAll(m => m.RemainingDuration <= 0);
    }

    public void TickModifiers()
    {
        foreach (var m in _modifiers)
        {
            m.Tick();
        }
    }

    public IEnumerable<StatData> GetStats()
    {
        return Stats.GetStats();
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
    // public override string ToString()
    // {
    //     return $@"[{Name}]
    //     health: {Health}
    //     attack: {Attack},
    //     defense: {Defense},
    //     magic: {Magic}
    //     moves: {Moves.Count}
    //     ";
    // }
}

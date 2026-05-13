using System.Collections.Generic;

public class CombatantStats
{
    private readonly Dictionary<StatType, CombatantStatData> _stats;
    public CombatantStats(float attack, float defense, float magic)
    {
        _stats = new Dictionary<StatType, CombatantStatData>
        {
            {
                StatType.Attack,
                new CombatantStatData(
                    StatType.Attack,
                    attack,
                    new List<ActiveModifier>())
            },
            {
                StatType.Defense,
                new CombatantStatData(
                    StatType.Defense,
                    defense,
                    new List<ActiveModifier>())
            },
            {
                StatType.Magic,
                new CombatantStatData(
                    StatType.Magic,
                    magic,
                    new List<ActiveModifier>())
            }
        };
    }

    public float GetStat(StatType statType)
    {
        if (!_stats.TryGetValue(statType, out var stat))
            throw new System.Exception($"Stat {statType} not found");
        return stat.GetCurrentValue();
    }
    // TODO: This is debugging
    public string GetActiveModifiersToString(StatType statType)
    {
        string result = "[";
        if (!_stats.TryGetValue(statType, out var stat))
            throw new System.Exception($"Stat {statType} not found");
        var modifiers = stat.GetActiveModifiers();
        foreach (var m in modifiers)
        {
            result += m.ToString() + ", ";
        }
        result += "]";
        return result;
    }

    public IEnumerable<CombatantStatData> GetStats()
    {
        return _stats.Values;
    }

    public void AddActiveModifier(ActiveModifier activeModifier)
    {
        _stats[activeModifier.Stat].AddActiveModifier(activeModifier);
    }

    public void RemoveExpiredModifiers()
    {
        foreach (var stat in _stats.Values)
        {
            stat.ActiveModifiers.RemoveAll(m => m.IsExpired);
        }
    }

    public void TickModifiers()
    {
        foreach (var stat in _stats.Values)
        {
            foreach (var m in stat.ActiveModifiers)
            {
                if (!m.HasOccured) { m.SetHasOccured(true); }

                m.SubstractRemainingDuration();
            }
        }
    }
}
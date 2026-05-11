using System.Collections.Generic;

public class Stats
{
    private readonly Dictionary<StatType, StatData> _stats;
    public Stats(float attack, float defense, float magic)
    {
        _stats = new Dictionary<StatType, StatData>
        {
            {
                StatType.Attack,
                new StatData(
                    StatType.Attack,
                    attack,
                    attack)
            },
            {
                StatType.Defense,
                new StatData(
                    StatType.Defense,
                    defense,
                    defense)
            },
            {
                StatType.Magic,
                new StatData(
                    StatType.Magic,
                    magic,
                    magic)
            }
        };
    }

    public IEnumerable<StatData> GetStats()
    {
        return _stats.Values;
    }

    public StatData GetStat(StatType type)
    {
        return _stats[type];
    }

    public void SetStatValue(StatType type, float value)
    {
        _stats[type].SetValue(value);
    }
}
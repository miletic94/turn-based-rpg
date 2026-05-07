using System.Collections.Generic;

public class StatsViewData
{
    private readonly Dictionary<StatType, StatData> _stats;

    public int AvailablePoints { get; private set; }

    public StatsViewData(Hero hero)
    {
        _stats = new Dictionary<StatType, StatData>
        {
            {
                StatType.Attack,
                new StatData(
                    StatType.Attack,
                    hero.Attack,
                    hero.Attack)
            },
            {
                StatType.Defense,
                new StatData(
                    StatType.Defense,
                    hero.Defense,
                    hero.Defense)
            },
            {
                StatType.Magic,
                new StatData(
                    StatType.Magic,
                    hero.Magic,
                    hero.Magic)
            }
        };

        AvailablePoints = hero.AvailableStatPoints;
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

    public void SetAvailablePoints(int value)
    {
        AvailablePoints = value;
    }
}
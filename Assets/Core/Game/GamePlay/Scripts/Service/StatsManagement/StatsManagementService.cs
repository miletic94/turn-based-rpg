using System.Collections.Generic;

public class StatsManagementService
{
    private readonly StatsViewData _statsViewData;

    public StatsManagementService(StatsViewData statsViewData)
    {
        _statsViewData = statsViewData;
    }

    public IEnumerable<StatData> GetStats()
    {
        return _statsViewData.GetStats();
    }

    public StatData GetStat(StatType statType)
    {
        return _statsViewData.GetStat(statType);
    }

    public int GetAvailablePoints()
    {
        return _statsViewData.AvailablePoints;
    }

    public void SetAvailablePoints(int value)
    {
        _statsViewData.SetAvailablePoints(value);
    }

    public float AddStat(StatType statType, float amount = 0.1f)
    {
        var stat = _statsViewData.GetStat(statType);

        var newValue = stat.CurrentValue + amount;

        _statsViewData.SetStatValue(statType, newValue);

        return newValue;
    }

    public float SubstractStat(StatType statType, float amount = 0.1f)
    {
        var stat = _statsViewData.GetStat(statType);

        var newValue = stat.CurrentValue - amount;

        _statsViewData.SetStatValue(statType, newValue);

        return newValue;
    }
}
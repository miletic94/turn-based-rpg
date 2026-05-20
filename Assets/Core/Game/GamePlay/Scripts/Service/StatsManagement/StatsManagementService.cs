using System.Collections.Generic;
using System.Data.Common;

// TODO: Services should be stateless. This one specifically had a bug that shows why
public class StatsManagementService
{

    public void IncreaseStat(StatManagementData data, StatType type)
    {
        AddStat(data, type);

        ChangeAvailablePoints(data, -1);
    }

    public void DecreaseStat(StatManagementData data, StatType type)
    {
        SubstractStat(data, type);

        ChangeAvailablePoints(data, +1);
    }

    private void ChangeAvailablePoints(StatManagementData data, int amount)
    {
        int current = data.AvailableStatPoints;

        data.SetAvaialableStatPoints(current + amount);
    }
    private float AddStat(StatManagementData data, StatType statType, float amount = 0.1f)
    {
        var stat = data.Stats.GetStat(statType);

        var newValue = stat.CurrentValue + amount;

        data.Stats.SetStatValue(statType, newValue);

        return newValue;
    }

    private float SubstractStat(StatManagementData data, StatType statType, float amount = 0.1f)
    {
        var stat = data.Stats.GetStat(statType);

        var newValue = stat.CurrentValue - amount;

        data.Stats.SetStatValue(statType, newValue);

        return newValue;
    }
}
using System.Collections.Generic;

// TODO: Services should be stateless. This one specifically had a bug that shows why
public class StatsManagementService
{
    private readonly GameplayContext _context;
    public StatsManagementService(GameplayContext context)
    {
        _context = context;
    }
    public IEnumerable<StatData> GetStats()
    {
        return _context.Hero.Stats.GetStats();
    }

    public StatData GetStat(StatType statType)
    {
        return _context.Hero.Stats.GetStat(statType);
    }

    public int GetAvailablePoints()
    {
        return _context.Hero.AvailableStatPoints;
    }

    public void IncreaseStat(StatType type)
    {
        AddStat(type);

        ChangeAvailablePoints(-1);
    }

    public void DecreaseStat(StatType type)
    {
        SubstractStat(type);

        ChangeAvailablePoints(+1);
    }

    private void ChangeAvailablePoints(int amount)
    {
        int current = GetAvailablePoints();

        SetAvailablePoints(current + amount);
    }

    private void SetAvailablePoints(int value)
    {
        _context.Hero.SetAvaialbleStatPoints(value);
    }

    private float AddStat(StatType statType, float amount = 0.1f)
    {
        var stat = _context.Hero.Stats.GetStat(statType);

        var newValue = stat.CurrentValue + amount;

        _context.Hero.Stats.SetStatValue(statType, newValue);

        return newValue;
    }

    private float SubstractStat(StatType statType, float amount = 0.1f)
    {
        var stat = _context.Hero.Stats.GetStat(statType);

        var newValue = stat.CurrentValue - amount;

        _context.Hero.Stats.SetStatValue(statType, newValue);

        return newValue;
    }
}
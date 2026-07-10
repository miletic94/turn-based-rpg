using System.Collections.Generic;

public class CombatantStatService
{
    public float GetStatValue(CombatantStatData stat)
    {
        var baseValue = stat.BaseValue;
        var activeModifiers = stat.ActiveModifiers;
        return GetStatValue(baseValue, activeModifiers);
    }

    public float GetStatValue(float baseValue, IEnumerable<ActiveModifier> activeModifiers)
    {
        var finalModifier = 1f;
        foreach (var modifier in activeModifiers)
        {
            finalModifier *= 1 + modifier.Value;
        }
        return baseValue * finalModifier;
    }

    public void RemoveExpiredModifiers(CombatantStats stats)
    {
        foreach (var stat in stats)
        {
            stat.ActiveModifiers.RemoveAll(m => m.IsExpired);
        }
    }
    public void TickModifiers(CombatantStats stats)
    {
        foreach (var stat in stats)
        {
            foreach (var m in stat.ActiveModifiers)
            {
                m.SubstractRemainingDuration();
            }
        }
    }
}
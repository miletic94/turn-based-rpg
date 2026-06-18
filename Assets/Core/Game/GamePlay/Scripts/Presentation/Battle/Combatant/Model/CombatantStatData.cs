using System.Collections.Generic;
using System.Linq;

public class CombatantStatData
{
    public StatType Type { get; }
    public float BaseValue { get; }
    public List<ActiveModifier> ActiveModifiers { get; }

    public CombatantStatData(
        StatType type,
        float baseValue,
        List<ActiveModifier> activeModifiers)
    {
        Type = type;
        BaseValue = baseValue;
        ActiveModifiers = activeModifiers;
    }

    public void AddActiveModifier(ActiveModifier activeModifier)
    {
        ActiveModifiers.Add(activeModifier);
    }
    public float GetCurrentValue()
    {
        var sum = ActiveModifiers.Sum(m =>
        {
            return m.Value;
        });
        return BaseValue + sum;
    }
}
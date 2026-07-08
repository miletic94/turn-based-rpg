using System;
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
        int idx = ActiveModifiers
            .FindIndex(modifier => modifier.Type == activeModifier.Type &&
                        modifier.Value == activeModifier.Value);

        if (idx == -1) ActiveModifiers.Add(activeModifier);
        else ActiveModifiers[idx] = activeModifier;
    }
    public float GetCurrentValue()
    {
        var sum = ActiveModifiers.Sum(m =>
        {
            // (1+m.Value)^StackNumber
            return (float)Math.Round(Math.Pow(1 + m.Value, m.StackNumber), 2);
        });
        return BaseValue + sum;
    }
}
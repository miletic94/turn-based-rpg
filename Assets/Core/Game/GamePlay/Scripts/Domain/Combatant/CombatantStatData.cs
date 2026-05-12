using System;
using System.Collections.Generic;
using System.Linq;

// TODO: Make this data structure part of Hero and Combatant
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
    // TODO: Domain knows about logic?
    public float GetCurrentValue()
    {
        return BaseValue + ActiveModifiers.Sum(m =>
        {
            return m.Value * (m.Type == ModifierType.Debuff ? -1 : 1);
        });
    }
}
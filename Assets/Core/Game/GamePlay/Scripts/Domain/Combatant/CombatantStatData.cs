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
        var str = "";
        var sum = ActiveModifiers.Sum(m =>
        {
            str += $"{m.Type} {m.Stat}: {m.Value * (m.Type == ModifierType.Debuff ? -1 : 1)}\n";
            return m.Value * (m.Type == ModifierType.Debuff ? -1 : 1);
        });
        str += $"sum of modifiers: {sum}\n";
        var total = BaseValue + sum;
        str += $"BaseValue: {BaseValue} Total: {total}";
        str += $"(int)(value * 10) = {(int)(total * 10)}";

        UnityEngine.Debug.Log(str);
        return BaseValue + sum;
    }
    public List<ActiveModifier> GetActiveModifiers()
    {
        return ActiveModifiers;
    }
}
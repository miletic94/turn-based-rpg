// TODO: Make this data structure part of Hero and Combatant
using System;

public class StatData
{
    public StatType Type { get; }

    public float CurrentValue { get; private set; }
    public float BaseValue { get; }

    public StatData(
        StatType type,
        float currentValue,
        float baseValue)
    {
        Type = type;
        CurrentValue = currentValue;
        BaseValue = baseValue;
    }

    public void SetValue(float value)
    {
        CurrentValue = value;
    }

    public bool CurrentGTBase => Math.Round(CurrentValue, 2) > Math.Round(BaseValue, 2);
}
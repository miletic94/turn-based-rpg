using System.Collections.Generic;

public class MoveEffect
{
    public List<HealthModifierEffect> HealthModifierEffects { get; }
    public List<StatModifierEffect> StatModifierEffects { get; }

    public MoveEffect(List<HealthModifierEffect> healthModifierEffects, List<StatModifierEffect> statModifierEffects)
    {
        HealthModifierEffects = healthModifierEffects;
        StatModifierEffects = statModifierEffects;
    }
}
using Newtonsoft.Json;

public class ScaledValue : IMoveEffectValue
{
    public float BaseValue { get; }
    public StatType ScalesOff { get; }
    public StatType ReducedBy { get; }

    public ScaledValue(float baseValue, StatType scalesOff, StatType reducedBy)
    {
        BaseValue = baseValue;
        ScalesOff = scalesOff;
        ReducedBy = reducedBy;
    }

    public float GetValue(EffectContext context)
    {
        var scalar = ScalesOff switch
        {
            StatType.Attack => context.Source.Stats.GetStat(StatType.Attack).CurrentValue,
            StatType.Defense => context.Source.Stats.GetStat(StatType.Defense).CurrentValue,
            StatType.Magic => context.Source.Stats.GetStat(StatType.Magic).CurrentValue,
            _ => 0
        };
        var reducer = ReducedBy switch
        {
            StatType.Attack => context.Target.Stats.GetStat(StatType.Attack).CurrentValue,
            StatType.Defense => context.Target.Stats.GetStat(StatType.Defense).CurrentValue,
            StatType.Magic => context.Target.Stats.GetStat(StatType.Magic).CurrentValue,
            _ => 0
        };
        return BaseValue * scalar * (1 - reducer);
    }
}
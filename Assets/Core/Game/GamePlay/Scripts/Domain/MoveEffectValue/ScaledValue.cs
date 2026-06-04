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

    // TODO: Data knows about logic?
    public float Get(EffectContext context)
    {
        var scalar = ScalesOff switch
        {
            StatType.Attack => context.Source.Stats.GetStat(StatType.Attack),
            StatType.Defense => context.Source.Stats.GetStat(StatType.Defense),
            StatType.Magic => context.Source.Stats.GetStat(StatType.Magic),
            _ => 0
        };
        var reducer = ReducedBy switch
        {
            StatType.Attack => context.Target.Stats.GetStat(StatType.Attack),
            StatType.Defense => context.Target.Stats.GetStat(StatType.Defense),
            StatType.Magic => context.Target.Stats.GetStat(StatType.Magic),
            _ => 0
        };

        return BaseValue * (scalar * (scalar / (scalar + reducer)));
    }

    public override string ToString()
    {
        return $"Value: {BaseValue}. Scales off {ScalesOff} "
        + (ReducedBy != StatType.None ? $"reduced by: {ReducedBy}" : "");
    }
}
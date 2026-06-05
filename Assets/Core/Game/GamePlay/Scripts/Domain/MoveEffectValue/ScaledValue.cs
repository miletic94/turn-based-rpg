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
        var scalar = ScalesOff != StatType.None ?
            context.Source.Stats.GetStat(ScalesOff)
            : 1f;

        var reducer = ReducedBy != StatType.None ?
            context.Target.Stats.GetStat(ReducedBy)
            : 0f;

        return BaseValue * (scalar * (scalar / (scalar + reducer)));
    }

    public override string ToString()
    {
        return $"Value: {BaseValue}. Scales off {ScalesOff} "
        + (ReducedBy != StatType.None ? $"reduced by: {ReducedBy}" : "");
    }
}
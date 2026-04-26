using Newtonsoft.Json;

public class ScaledValue : IValue
{
    public float BaseValue { get; }
    public StatType ScalseOff { get; }
    public StatType ReducedBy { get; }

    public ScaledValue(float baseValue, StatType scalesOff, StatType reducedBy)
    {
        BaseValue = baseValue;
        ScalseOff = scalesOff;
        ReducedBy = reducedBy;
    }

    public float GetValue(EffectContext context)
    {
        // TODO: Abstract this so we don't have to add new switch case if we add StatType
        var scalar = ScalseOff switch
        {
            StatType.Attack => context.Source.Attack,
            StatType.Defense => context.Source.Defense,
            StatType.Magic => context.Source.Magic,
            _ => 0
        };
        var reducer = ReducedBy switch
        {
            StatType.Attack => context.Source.Attack,
            StatType.Defense => context.Source.Defense,
            StatType.Magic => context.Source.Magic,
            _ => 0
        };
        return BaseValue * scalar - reducer;
    }
}
public class FlatValue : IMoveEffectValue
{
    public float BaseValue { get; }
    public FlatValue(float baseValue)
    {
        BaseValue = baseValue;
    }

    public float Get(EffectContext context)
    {
        return BaseValue;
    }
    public override string ToString()
    {
        return $"Value: {BaseValue}";
    }
}
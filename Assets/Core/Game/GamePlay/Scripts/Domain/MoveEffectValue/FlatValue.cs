public class FlatValue : IMoveEffectValue
{
    public float BaseValue { get; }
    public FlatValue(float baseValue)
    {
        BaseValue = baseValue;
    }

    public float GetValue(EffectContext context)
    {
        return BaseValue;
    }
}
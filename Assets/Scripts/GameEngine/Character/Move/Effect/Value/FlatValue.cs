public class FlatValue : IValue
{
    public int BaseValue { get; }

    public FlatValue(int baseValue)
    {
        BaseValue = baseValue;
    }

    public float GetValue(EffectContext context)
    {
        return BaseValue;
    }
}
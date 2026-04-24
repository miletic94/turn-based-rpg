public class FlatValue : IValue
{
    public int BaseValue { get; }

    public FlatValue(int baseValue)
    {
        BaseValue = baseValue;
    }

    public int GetValue(EffectContext context)
    {
        return BaseValue;
    }
}
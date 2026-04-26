using Newtonsoft.Json;

public class FlatValue : IValue
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
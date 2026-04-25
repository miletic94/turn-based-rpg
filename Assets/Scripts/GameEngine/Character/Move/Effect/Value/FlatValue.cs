using Newtonsoft.Json;

public class FlatValue : IValue
{
    public int BaseValue { get; }

    [JsonConstructor]
    public FlatValue(int baseValue)
    {
        BaseValue = baseValue;
    }

    public float GetValue(EffectContext context)
    {
        return BaseValue;
    }
}
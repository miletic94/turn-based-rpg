public class ReferenceValue : IValue
{
    public string Reference { get; }

    public ReferenceValue(string reference)
    {
        Reference = reference;
    }

    public float GetValue(EffectContext context)
    {
        return context.GetResult(Reference);
    }
}
using Newtonsoft.Json;

public class ReferenceValue : IValue
{
    public string SourceId { get; }

    public ReferenceValue(string sourceId)
    {
        SourceId = sourceId;
    }

    public float GetValue(EffectContext context)
    {
        return context.GetResult(SourceId);
    }
}
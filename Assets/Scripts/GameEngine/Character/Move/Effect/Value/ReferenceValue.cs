using Newtonsoft.Json;

public class ReferenceValue : IValue
{
    public string SourceId { get; }

    [JsonConstructor]
    public ReferenceValue(string sourceId)
    {
        SourceId = sourceId;
    }

    public float GetValue(EffectContext context)
    {
        return context.GetResult(SourceId);
    }
}
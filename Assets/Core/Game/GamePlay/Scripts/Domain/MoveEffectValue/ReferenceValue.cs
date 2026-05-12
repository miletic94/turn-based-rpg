using Newtonsoft.Json;

public class ReferenceValue : IMoveEffectValue
{
    public string SourceId { get; }

    public ReferenceValue(string sourceId)
    {
        SourceId = sourceId;
    }

    public float Get(EffectContext context)
    {
        return context.GetResult(SourceId);
    }
}
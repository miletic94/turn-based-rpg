public class HealthModifierValue
{
    public float? BaseValue { get; }
    public int? SourceId { get; }
    public HealthModifierValue(float? baseValue, int? sourceId)
    {
        BaseValue = baseValue;
        SourceId = sourceId;
    }
}
public class HealthModifierValue
{
    public HealthModifierValueType Type;
    public float? BaseValue;
    public int? SourceId;

    public string Description => Type == HealthModifierValueType.Scaled ?
        $"{BaseValue}" : "the same";
}
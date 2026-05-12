public interface IMoveEffect
{
    string Id { get; }
    TargetType Target { get; }
    IMoveEffectValue Value { get; }
    bool IsSource { get; }
}
public interface IMoveEffect
{
    string Id { get; }
    TargetType Target { get; }
    public bool IsSource { get; }
    public IMoveEffectValue Value { get; }

    void Execute(EffectContext context);
}
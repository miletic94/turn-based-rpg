#nullable enable
public interface IMoveEffect
{
    string Id { get; }
    TargetType Target { get; }
    public bool IsSource { get; }

    void Execute(EffectContext context);
}
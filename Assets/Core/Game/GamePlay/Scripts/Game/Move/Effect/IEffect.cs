#nullable enable
public interface IEffect
{
    string Id { get; }
    TargetType Target { get; }
    public bool IsSource { get; }

    void Execute(EffectContext context);
}
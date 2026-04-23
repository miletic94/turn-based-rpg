public interface IEffect
{
    string? Id { get; }
    TargetType Target { get; }

    void Execute(EffectContext context);
}
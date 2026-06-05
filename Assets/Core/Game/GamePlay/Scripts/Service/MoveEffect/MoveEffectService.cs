using System;
public struct EffectResult
{
    public Combatant Target { get; }
    public float Value { get; }
    public EffectResult(Combatant target, float value)
    {
        Target = target;
        Value = value;
    }
}
public class MoveEffectService
{
    public IEffectResult Apply(IMoveEffect effect, EffectContext context)
    {
        var target = context.ResolveTarget(effect.Target);

        float value = effect.Value.Get(context);

        if (effect.IsSource)
            context.StoreResult(effect.Id, value);

        return effect.Apply(target, value);
    }
}
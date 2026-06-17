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
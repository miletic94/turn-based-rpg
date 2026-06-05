using System.Collections.Generic;

public class MoveService
{
    private readonly MoveEffectService _moveEffectService;

    public MoveService(
        MoveEffectService effectExecutionService)
    {
        _moveEffectService = effectExecutionService;
    }

    public List<IEffectResult> ApplyMove(
        Combatant source,
        Combatant target,
        Move move)
    {
        var context = CreateEffectContext(source, target);
        return ApplyEffects(move, context);
    }

    private EffectContext CreateEffectContext(
        Combatant source,
        Combatant target)
    {
        return new EffectContext(source, target);
    }

    private List<IEffectResult> ApplyEffects(
        Move move,
        EffectContext context)
    {
        List<IEffectResult> results = new();
        foreach (var effect in move.Effects)
        {
            results.Add(_moveEffectService.Apply(effect, context));
        }
        return results;
    }
}
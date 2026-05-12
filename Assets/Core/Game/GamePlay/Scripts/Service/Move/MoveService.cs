using System.Collections.Generic;

public class MoveService
{
    private readonly MoveEffectService _moveEffectService;

    public MoveService(
        MoveEffectService effectExecutionService)
    {
        _moveEffectService = effectExecutionService;
    }

    public void ApplyMove(
        Combatant source,
        Combatant target,
        Move move)
    {
        var context = CreateEffectContext(source, target);
        ApplyEffects(move, context);
    }

    public void TickModifiers(Combatant combatant)
    {
        _moveEffectService.TickModifiers(combatant);
    }

    public void RemoveExpiredModifiers(Combatant combatant)
    {
        _moveEffectService.RemoveExpiredModifiers(combatant);
    }

    private EffectContext CreateEffectContext(
        Combatant source,
        Combatant target)
    {
        return new EffectContext(source, target);
    }

    private void ApplyEffects(
        Move move,
        EffectContext context)
    {
        foreach (var effect in move.Effects)
        {
            _moveEffectService.Apply(effect, context);
        }
    }
}
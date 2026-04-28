using System;
using UnityEngine;

public class MoveExecutor
{
    public void Execute(Character source, Character target, Move move)
    {
        if (!source.HasMove(move))
            throw new Exception($"{source.Name} doesn't have move {move.Name}");
        if (!source.HasEnoughResource(move))
            throw new Exception($"{source.Name} doesn't have enough resources. Needs: {move.Cost.Amount} {move.Cost.Type}");

        source.TickModifiers();

        var ctx = new EffectContext(source, target);
        foreach (var effect in move.Effects)
        {
            effect.Execute(ctx);
        }
        source.ReduceResource(move.Cost);

        source.ClearInactiveModifiers();

        foreach (var change in ctx.Changes)
        {
            Debug.Log($@"
                Target: {change.Target};
                Stat: {change.Stat};
                Before: {change.Before};
                After: {change.After};
            ");
        }
    }
}
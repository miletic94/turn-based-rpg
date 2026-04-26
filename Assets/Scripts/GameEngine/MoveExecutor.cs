using System;

public class MoveExecutor
{
    public void Execute(Character source, Character target, Move move)
    {
        if (!source.HasMove(move))
            throw new Exception($"{source.Name} doesn't have move {move.Name}");

        source.TickModifiers();

        var ctx = new EffectContext(source, target);
        foreach (var effect in move.Effects)
        {
            effect.Execute(ctx);
        }

        source.ClearInactiveModifiers();
    }
}
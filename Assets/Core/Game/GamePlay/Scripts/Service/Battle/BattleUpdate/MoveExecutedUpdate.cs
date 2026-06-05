using System.Collections.Generic;

public sealed class MoveExecutedUpdate : BattleUpdate
{
    public Combatant Actor { get; }
    public Combatant Target { get; }
    public List<IEffectResult> MoveResult { get; }


    public MoveExecutedUpdate(Combatant actor, Combatant target, List<IEffectResult> moveResult)
    {
        Actor = actor;
        Target = target;
        MoveResult = moveResult;
    }
}
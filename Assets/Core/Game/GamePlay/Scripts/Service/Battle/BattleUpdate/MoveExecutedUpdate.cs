using System.Collections.Generic;

public sealed class MoveExecutedUpdate : BattleUpdate
{
    public Combatant Actor { get; }
    public Combatant Target { get; }
    public MoveEffect MoveEffect { get; }


    public MoveExecutedUpdate(Combatant actor, Combatant target, MoveEffect moveEffect)
    {
        Actor = actor;
        Target = target;
        MoveEffect = moveEffect;
    }
}
public sealed class MoveExecutedUpdate : BattleUpdate
{
    public Combatant Actor { get; }
    public Combatant Target { get; }
    public Move Move { get; }

    public MoveExecutedUpdate(Combatant actor, Combatant target, Move move)
    {
        Actor = actor;
        Target = target;
        Move = move;
    }
}
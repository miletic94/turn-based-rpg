public sealed class TurnStartedUpdate : BattleUpdate
{
    public Combatant Actor { get; }
    public Combatant Target { get; }
    public Move Move { get; }

    public TurnStartedUpdate(Combatant actor, Combatant target)
    {
        Actor = actor;
        Target = target;
    }
}
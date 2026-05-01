public class MoveSelectedEvent : IGameEvent
{
    public Combatant Actor { get; }
    public Move Move { get; }

    public MoveSelectedEvent(Combatant actor, Move move)
    {
        Actor = actor;
        Move = move;
    }
}
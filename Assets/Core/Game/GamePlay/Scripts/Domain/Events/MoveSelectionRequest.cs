public class MoveSelectionRequestedEvent : IGameEvent
{
    public Combatant Actor { get; }

    public MoveSelectionRequestedEvent(Combatant actor)
    {
        Actor = actor;
    }
}
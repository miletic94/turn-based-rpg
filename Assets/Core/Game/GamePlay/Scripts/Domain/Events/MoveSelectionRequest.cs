public class MoveSelectionRequestedEvent : IGameEvent
{
    public Character Actor { get; }

    public MoveSelectionRequestedEvent(Character actor)
    {
        Actor = actor;
    }
}
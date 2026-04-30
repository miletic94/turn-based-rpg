public class MoveSelectedEvent : IGameEvent
{
    public Character Actor { get; }
    public Move Move { get; }

    public MoveSelectedEvent(Character actor, Move move)
    {
        Actor = actor;
        Move = move;
    }
}
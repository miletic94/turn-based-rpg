public class MoveExecutedEvent : IGameEvent
{
    public Character Source { get; }
    public Character Target { get; }
    public Move Move { get; }

    public MoveExecutedEvent(
        Character source,
        Character target,
        Move move)
    {
        Source = source;
        Target = target;
        Move = move;
    }
}
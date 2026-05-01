public class MoveExecutedEvent : IGameEvent
{
    public Combatant Source { get; }
    public Combatant Target { get; }
    public Move Move { get; }

    public MoveExecutedEvent(
        Combatant source,
        Combatant target,
        Move move)
    {
        Source = source;
        Target = target;
        Move = move;
    }
}
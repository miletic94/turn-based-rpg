public class MovePayload : IDraggablePayload
{
    public Move Move { get; private set; }
    public MovePayload(Move move)
    {
        Move = move;
    }
}
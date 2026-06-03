using UnityEngine;

public class MoveItemData : IIdentifiable
{
    public int Id { get; }
    public Move Move;
    public Sprite IconSprite { get; }

    public MoveItemData(int id, Move move, Sprite sprite)
    {
        Id = id;
        Move = move;
        IconSprite = sprite;
    }
}
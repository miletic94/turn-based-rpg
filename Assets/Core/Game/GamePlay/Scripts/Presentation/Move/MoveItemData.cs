using UnityEngine;

public class MoveItemData : IIdentifiable
{
    public int Id { get; }

    public Sprite IconSprite { get; }

    public MoveItemData(int id, Sprite sprite)
    {
        Id = id;
        IconSprite = sprite;
    }
}
using UnityEngine;

public class BattleMoveItemData : IIdentifiable
{
    public int Id { get; }
    public Sprite IconSprite;

    public BattleMoveItemData(int id, Sprite sprite)
    {
        Id = id;
        IconSprite = sprite;
    }
}
using UnityEngine;

public class RewardItemData : IIdentifiable
{
    public int Id { get; }

    public Sprite IconSprite { get; }

    public RewardItemData(int id, Sprite sprite)
    {
        Id = id;
        IconSprite = sprite;
    }
}

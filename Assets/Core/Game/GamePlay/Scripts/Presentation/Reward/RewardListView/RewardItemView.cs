using System;
using UnityEngine;
using UnityEngine.UI;
public class RewardItemView : ListItemView<RewardItemData>
{
    [SerializeField] Image _icon;
    [SerializeField] ClickableUI _clickable;
    public override void ShowData(RewardItemData data)
    {
        _icon.sprite = data.IconSprite;
    }

    public void BindClick(Action callback)
    {
        _clickable.Bind(callback);
    }
}
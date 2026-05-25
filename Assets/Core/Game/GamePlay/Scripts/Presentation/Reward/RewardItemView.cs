using System;
using UnityEngine;
public class RewardItemView : ListItemView<RewardItemData>
{
    [SerializeField] MoveView1 _moveView;
    [SerializeField] ClickableUI _clickable;
    public override void ShowData(RewardItemData data)
    {
        _moveView.SetSprite(data.IconSprite);
    }

    public void BindClick(Action callback)
    {
        _clickable.Bind(callback);
    }
}
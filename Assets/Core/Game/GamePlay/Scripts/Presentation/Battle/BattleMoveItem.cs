using System;
using UnityEngine;
public class BattleMoveItem : ListItemView<BattleMoveItemData>
{
    [SerializeField] MoveView1 _moveView;
    [SerializeField] ClickableUI _clickable;
    public override void ShowData(BattleMoveItemData data)
    {
        _moveView.SetSprite(data.IconSprite);
    }

    public void BindClick(Action callback)
    {
        _clickable.Bind(callback);
    }
}
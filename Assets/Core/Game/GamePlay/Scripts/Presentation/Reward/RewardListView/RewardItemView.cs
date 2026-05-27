using System;
using UnityEngine;
public class RewardItemView : MoveListItem
{
    [SerializeField] ClickableUI _clickable;

    public void BindClick(Action callback)
    {
        _clickable.Bind(callback);
    }
}
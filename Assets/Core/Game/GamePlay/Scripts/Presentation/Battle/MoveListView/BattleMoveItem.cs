using System;
using UnityEngine;
using UnityEngine.UI;
public class BattleMoveItem : MonoBehaviour, IListItemView<BattleMoveItemData>
{
    [SerializeField] Image _icon;
    [SerializeField] ClickableUI _clickable;
    public void ShowData(BattleMoveItemData data)
    {
        _icon.sprite = data.IconSprite;
    }

    public void BindClick(Action callback)
    {
        _clickable.Bind(callback);
    }
}
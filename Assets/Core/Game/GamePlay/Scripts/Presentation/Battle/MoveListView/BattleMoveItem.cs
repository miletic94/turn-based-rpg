using System;
using UnityEngine;
public class BattleMoveItem :
MoveListItem,
IHoverDelayedDataSource
{
    [SerializeField] ClickableUI _clickable;
    [SerializeField] HoverableUI _hoverable;
    [SerializeField] RectTransform _rectTransform;
    private Action<HoverData> _onHoverDelayed;
    private Action<HoverData> _onHoverExited;

    public void BindClick(Action<Move> callback)
    {
        _clickable.Bind(() => callback.Invoke(_data.Move));
    }

    public object GetHoverDelayedData()
    {
        return new MoveHoverData
        {
            MoveId = _data.Id,
            RectTransform = _rectTransform
        };
    }

    public void BindHoverable(Action<HoverData> onHoverDelayed,
        Action<HoverData> onHoverExited)
    {
        UnbindHoverable();
        _onHoverDelayed = onHoverDelayed;
        _onHoverExited = onHoverExited;
        _hoverable.HoverDelayed += _onHoverDelayed;
        _hoverable.HoverExited += _onHoverExited;
    }

    private void UnbindHoverable()
    {
        _hoverable.HoverDelayed -= _onHoverDelayed;
        _hoverable.HoverExited -= _onHoverExited;
    }
}
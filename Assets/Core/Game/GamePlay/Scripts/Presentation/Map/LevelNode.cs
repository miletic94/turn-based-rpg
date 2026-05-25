using System;
using UnityEngine;

public class LevelNode : ListItemView<LevelNodeData>
{
    [SerializeField] TextLabelUI _textLabel;
    [SerializeField] ClickableUI _clickable;
    public override void ShowData(LevelNodeData data)
    {
        _textLabel.SetText(data.LevelNumber.ToString());
    }

    public void BindClick(Action callback)
    {
        _clickable.Bind(callback);
    }
}
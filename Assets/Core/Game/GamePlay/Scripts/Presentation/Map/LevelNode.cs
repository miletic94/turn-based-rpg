using System;
using TMPro;
using UnityEngine;

public class LevelNode : ListItemView<LevelNodeData>
{
    [SerializeField] TMP_Text _textLabel;
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
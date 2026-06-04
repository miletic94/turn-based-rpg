using System;
using TMPro;
using UnityEngine;

public class LevelNode : MonoBehaviour, IListItemView<LevelNodeData>
{
    [SerializeField] TMP_Text _textLabel;
    [SerializeField] ClickableUI _clickable;
    private LevelNodeData _levelNodeData;
    public void ShowData(LevelNodeData data)
    {
        _levelNodeData = data;
        _textLabel.SetText(data.LevelNumber.ToString());
    }

    public void BindClick(Action<Character> callback)
    {
        _clickable.Bind(() => callback.Invoke(_levelNodeData.Enemy));
    }
}
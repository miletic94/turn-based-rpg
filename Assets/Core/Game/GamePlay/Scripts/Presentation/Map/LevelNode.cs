using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelNode : MonoBehaviour, IListItemView<LevelNodeData>
{
    [SerializeField] TMP_Text _textLabel;
    [SerializeField] ClickableUI _clickable;
    [SerializeField] private Color _originalColor;
    [SerializeField] private Color _beatenColor;
    [SerializeField] private Image _backgroundImage;

    private LevelNodeData _levelNodeData;
    public void ShowData(LevelNodeData data)
    {
        _levelNodeData = data;
        SetInteractable(data.Interactable);
        _textLabel.SetText(data.LevelNumber.ToString());
    }
    private void SetInteractable(bool interactable)
    {
        _clickable.SetInteractable(interactable);
        var color = interactable ? _originalColor : _beatenColor;
        color.a = 1f;
        _backgroundImage.color = color;
    }
    public void BindClick(Action<Character> callback)
    {
        _clickable.Bind(() => callback.Invoke(_levelNodeData.Enemy));
    }
}
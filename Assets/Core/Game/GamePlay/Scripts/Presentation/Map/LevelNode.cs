using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelNode : MonoBehaviour, IListItemView<LevelNodeData>
{
    [SerializeField] TMP_Text _textLabel;
    [SerializeField] ClickableUI _clickable;
    private Color _originalColor;
    private Image _image;

    private LevelNodeData _levelNodeData;
    public void ShowData(LevelNodeData data)
    {
        _levelNodeData = data;
        _image = GetComponent<Image>();
        _originalColor = _image.color;
        SetInteractable(data.Interactable);
        _textLabel.SetText(data.LevelNumber.ToString());
    }
    private void SetInteractable(bool interactable)
    {
        _clickable.SetInteractable(interactable);
        var color = interactable ? _originalColor : _originalColor * 0.7f;
        color.a = 1f;
        _image.color = color;
    }
    public void BindClick(Action<Character> callback)
    {
        _clickable.Bind(() => callback.Invoke(_levelNodeData.Enemy));
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTreeNodeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _button;

    private Action _onClick;

    public void SetText(string text)
    {
        _label.text = text;
    }

    public void SetInteractable(bool interactable)
    {
        _button.interactable = interactable;
    }

    public void SetClickAction(Action onClick)
    {
        _onClick = onClick;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(HandleClicked);
    }

    private void HandleClicked()
    {
        _onClick?.Invoke();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(HandleClicked);
    }
}
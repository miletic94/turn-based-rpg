using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableUI : MonoBehaviour, IPointerClickHandler
{
    public event Action Clicked;

    private bool _isInteractable = false;
    public void SetInteractable(bool isInteractable)
    {
        _isInteractable = isInteractable;
    }

    public void Bind(Action onClicked)
    {
        Clicked = onClicked;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isInteractable) return;

        InvokeClick();
    }

    private void InvokeClick()
    {
        if (!_isInteractable)
            return;

        Clicked?.Invoke();
    }
}
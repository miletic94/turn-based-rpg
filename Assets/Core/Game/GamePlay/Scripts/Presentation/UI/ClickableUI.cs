using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableUI : MonoBehaviour, IPointerClickHandler
{
    public event Action Clicked;

    public bool IsInteractable = true;

    public void Bind(Action onClicked)
    {
        Clicked = onClicked;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsInteractable) return;

        InvokeClick();
    }

    private void InvokeClick()
    {
        if (!IsInteractable)
            return;

        Clicked?.Invoke();
    }
}
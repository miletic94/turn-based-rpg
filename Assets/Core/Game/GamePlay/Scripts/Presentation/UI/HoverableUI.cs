using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverableUI : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public event Action OnHoverEntered;
    public event Action OnHoverExited;


    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEntered?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExited?.Invoke();
    }
}
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class MoveButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text label;
    private Action _onHover;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _onHover.Invoke();
        Debug.Log("Hover Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hover Exit");

    }

    public void SetText(string text)
    {
        label.text = text;
    }

    public void SetOnClick(Action onClick)
    {
        _button.onClick.AddListener(() => onClick());
    }

    public void SetOnHover(Action onHover)
    {
        _onHover = onHover;
    }
}
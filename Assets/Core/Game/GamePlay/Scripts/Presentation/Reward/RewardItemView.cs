using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private Button button;
    private Action _onHovered;

    public void SetText(string text)
    {
        label.text = text;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _onHovered.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hover Exit");
    }

    public void SetOnRewardSelected(Action onCliked)
    {
        button.onClick.AddListener(() => onCliked());
    }
    public void SetOnRewardHovered(Action onHovered)
    {
        _onHovered = onHovered;
    }
}
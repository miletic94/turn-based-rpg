using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private Button button;

    public void SetText(string text)
    {
        label.text = text;
    }
    public void SetButtonAction(Action action)
    {
        button.onClick.AddListener(() => action());
    }
}
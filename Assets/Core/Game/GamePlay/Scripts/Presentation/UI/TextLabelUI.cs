using TMPro;
using UnityEngine;

public class TextLabelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    public void SetText(string text)
    {
        label.text = text;
    }
    public void SetColor(Color color)
    {
        label.color = color;
    }
}
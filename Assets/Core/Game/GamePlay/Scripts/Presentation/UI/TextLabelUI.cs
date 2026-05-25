using TMPro;
using UnityEngine;


// TODO: Redundant to TMP_Text. Delete
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
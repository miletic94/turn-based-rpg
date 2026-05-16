using TMPro;
using UnityEngine;

public class TooltipView : MonoBehaviour
{
    [SerializeField] TMP_Text _tooltipTextContainer;

    public void SetText(string text)
    {
        _tooltipTextContainer.text = text;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
using TMPro;
using UnityEngine;

public class MoveDescriptionTooltipView : MonoBehaviour, ITooltipView<string>
{
    [SerializeField] TMP_Text _tooltipTextContainer;

    public void Show(string data)
    {
        _tooltipTextContainer.text = data;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
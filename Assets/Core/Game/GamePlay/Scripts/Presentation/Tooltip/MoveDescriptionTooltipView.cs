using TMPro;
using UnityEngine;

public class MoveDescriptionTooltipView :
    MonoBehaviour,
    ITooltipView<MoveDescriptionTooltipMessage>
{
    [SerializeField] TMP_Text _tooltipTextContainer;
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] private Vector2 _offset;


    public void Show(MoveDescriptionTooltipMessage data)
    {
        _tooltipTextContainer.text = data.MoveDescription;

        PositionTooltip(data.Anchor);

        gameObject.SetActive(true);
    }

    public void Hide(HideMessage _)
    {
        gameObject.SetActive(false);
    }

    private void PositionTooltip(RectTransform anchor)
    {
        Vector3[] corners = new Vector3[4];
        anchor.GetWorldCorners(corners);

        Vector3 position = corners[2];

        _rectTransform.position = position + (Vector3)_offset;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
using TMPro;
using UnityEngine;

public class MoveDescriptionTooltipView :
    MonoBehaviour,
    ITooltipView<MoveDescriptionTooltipMessage>
{
    [SerializeField] TMP_Text _tooltipTextContainer;
    [SerializeField] RectTransform _rectTransform;


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

        var screenPos = RectTransformUtility.WorldToScreenPoint(null, corners[2]);
        bool isUpperHalf = screenPos.y > UnityEngine.Screen.height * 0.5f;

        if (isUpperHalf)
        {
            var bottomCenter = (corners[0] + corners[3]) * 0.5f;

            _rectTransform.pivot = new Vector2(0.5f, 1f);
            _rectTransform.position = bottomCenter;
        }
        else
        {
            var topCenter = (corners[1] + corners[2]) * 0.5f;

            _rectTransform.pivot = new Vector2(0.5f, 0f);
            _rectTransform.position = topCenter;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
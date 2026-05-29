using UnityEngine;

public readonly struct MoveDescriptionTooltipMessage
    : IUIFeedbackMessage
{
    public string MoveDescription { get; }

    public RectTransform Anchor { get; }

    public MoveDescriptionTooltipMessage(
        string moveDescription,
        RectTransform anchor)
    {
        MoveDescription = moveDescription;
        Anchor = anchor;
    }
}
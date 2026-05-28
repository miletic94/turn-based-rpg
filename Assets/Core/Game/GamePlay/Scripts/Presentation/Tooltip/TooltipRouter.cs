using UnityEngine;

public class TooltipRouter : MonoBehaviour
{
    private UIFeedbackBus _uiFeedbackBus;
    [SerializeField] WarningTooltipView warningTooltipView;
    public void Initialize(UIFeedbackBus uiFeedbackBus)
    {
        _uiFeedbackBus = uiFeedbackBus;
        _uiFeedbackBus.Subscribe<WarningMessage>(
            warningTooltipView.Show);
    }
}
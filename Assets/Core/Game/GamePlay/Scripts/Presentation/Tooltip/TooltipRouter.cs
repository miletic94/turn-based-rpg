using UnityEngine;

public class TooltipRouter : MonoBehaviour
{
    private UIFeedbackBus _uiFeedbackBus;
    [SerializeField] WarningTooltipView _warningTooltipView;
    [SerializeField] MoveDescriptionTooltipView _moveDescriptionTooltipView;
    public void Initialize(UIFeedbackBus uiFeedbackBus)
    {
        _uiFeedbackBus = uiFeedbackBus;

        _uiFeedbackBus.Subscribe<WarningMessage>(
            _warningTooltipView.Show);
        _uiFeedbackBus.Subscribe<MoveDescriptionTooltipMessage>(
            _moveDescriptionTooltipView.Show);
        _uiFeedbackBus.Subscribe<HideMessage>(
            _moveDescriptionTooltipView.Hide);
    }
}
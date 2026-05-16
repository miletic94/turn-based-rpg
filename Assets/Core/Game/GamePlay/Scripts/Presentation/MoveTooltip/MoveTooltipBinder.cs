public class MoveTooltipBinder
{
    private readonly MoveDescriptionService _moveDescriptionService;
    private readonly TooltipView _tooltipView;

    public MoveTooltipBinder(
        MoveDescriptionService descriptions,
        TooltipView tooltip)
    {
        _moveDescriptionService = descriptions;
        _tooltipView = tooltip;
    }

    public void BindTooltip(Move move, MoveView moveView)
    {
        moveView.MakeHoverable(
            onHoverDelayed: () => ShowTooltip(move),
            onHoverExited: HideTooltip);
    }

    private void ShowTooltip(Move move)
    {
        _tooltipView.SetText(
            _moveDescriptionService.Describe(move));

        _tooltipView.Show();
    }

    private void HideTooltip()
    {
        _tooltipView.Hide();
    }
}
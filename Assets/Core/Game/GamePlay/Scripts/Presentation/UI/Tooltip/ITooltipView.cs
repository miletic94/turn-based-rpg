public interface ITooltipView<TData>
{
    void Show(TData data);
    void Hide();
}
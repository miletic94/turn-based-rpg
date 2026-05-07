public class StatsManagementController
{
    private readonly StatsManagementView _view;
    private readonly StatsManagementService _service;

    public StatsManagementController(
        StatsManagementView view,
        StatsManagementService service)
    {
        _view = view;
        _service = service;
    }

    public void Initialize()
    {
        _view.Initialize(
            _service.GetStats(),
            _service.GetAvailablePoints(),
            OnPlusClicked,
            OnMinusClicked);
    }

    private void OnPlusClicked(StatType type)
    {
        _service.AddStat(type);
        _service.SetAvailablePoints(_service.GetAvailablePoints() - 1);

        Refresh();
    }

    private void OnMinusClicked(StatType type)
    {
        _service.SubstractStat(type);
        _service.SetAvailablePoints(_service.GetAvailablePoints() + 1);

        Refresh();
    }

    private void Refresh()
    {
        _view.Refresh(
            _service.GetStats(),
            _service.GetAvailablePoints());
    }
}
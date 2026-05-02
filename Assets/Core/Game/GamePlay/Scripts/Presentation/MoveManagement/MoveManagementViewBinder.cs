using System;

public class MoveManagementViewBinder
{
    private readonly IMoveManagementView _view;
    private readonly MoveLoadout _loadout;
    private readonly MoveLoadoutService _service;

    private Action _onSave;

    public MoveManagementViewBinder(
        IMoveManagementView view,
        MoveLoadout loadout,
        MoveLoadoutService service)
    {
        _view = view;
        _loadout = loadout;
        _service = service;
    }

    public void Bind(Action onSave)
    {
        _onSave = onSave;

        _view.BindDropZones(
            HandleDrop);

        _view.BindSave(
            HandleSave);

        Refresh();
    }

    public void Unbind()
    {
        _view.Unbind();
    }

    private void HandleDrop(
        MoveItemView item,
        MoveDropZone.ZoneType zone)
    {
        bool success = false;

        switch (zone)
        {
            case MoveDropZone.ZoneType.Equipped:
                success =
                    _service.MoveToEquipped(
                        item.MoveData);
                break;

            case MoveDropZone.ZoneType.Available:
                success =
                    _service.MoveToAvailable(
                        item.MoveData);
                break;
        }

        if (!success)
        {
            item.ReturnToOriginalParent();
            return;
        }

        Refresh();
    }

    private void HandleSave()
    {
        _onSave?.Invoke();
    }

    private void Refresh()
    {
        _view.Show(
            _loadout.AvailableMoves,
            _loadout.EquippedMoves);
    }
}
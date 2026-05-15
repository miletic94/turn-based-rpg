using System;
using System.Collections.Generic;

public class MoveManagementController
{
    private readonly MoveManagementView _view;
    private readonly MoveLoadoutService _service;

    private Action<List<Move>, List<Move>> _onSave;

    public MoveManagementController(
        MoveManagementView view,
        MoveLoadoutService service,
        Action<List<Move>, List<Move>> onSave)
    {
        _view = view;
        _service = service;
        _onSave = onSave;
    }


    public void Bind()
    {
        _view.OnMoveDropped += HandleDrop;
        _view.SaveClicked += HandleSave;

        Refresh();
    }

    public void Unbind()
    {
        _view.OnMoveDropped -= HandleDrop;
        _view.SaveClicked -= HandleSave;
    }

    private void HandleDrop(
        MovePayload movePayload,
        MoveDropZoneType zoneType)
    {
        bool success = zoneType switch
        {
            MoveDropZoneType.Equipped =>
                _service.MoveToEquipped(movePayload.Move),

            MoveDropZoneType.Available =>
                _service.MoveToAvailable(movePayload.Move),

            _ => false
        };

        if (!success)
        {
            _view.ResetDrag(movePayload.Move);
            return;
        }

        Refresh();
    }

    private void HandleSave()
    {
        _onSave?.Invoke(_service.AvailableMoves, _service.EquippedMoves);
    }

    private void Refresh()
    {
        _view.Render(
            _service.AvailableMoves,
            _service.EquippedMoves);
    }
}
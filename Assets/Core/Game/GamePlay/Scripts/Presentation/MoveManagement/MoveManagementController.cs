using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveManagementController
{
    private readonly IMoveManagementView _view;
    private readonly MoveLoadoutService _service;

    private Action<List<Move>, List<Move>> _onSave;

    public MoveManagementController(
        IMoveManagementView view,
        MoveLoadoutService service,
        Action<List<Move>, List<Move>> onSave)
    {
        _view = view;
        _service = service;
        _onSave = onSave;
    }


    public void Bind()
    {
        _view.MoveDropped += HandleDrop;
        _view.SaveClicked += HandleSave;

        Refresh();
    }

    public void Unbind()
    {
        _view.MoveDropped -= HandleDrop;
        _view.SaveClicked -= HandleSave;
    }

    private void HandleDrop(
        Move move,
        MoveDropZone.ZoneType zone)
    {
        bool success = zone switch
        {
            MoveDropZone.ZoneType.Equipped =>
                _service.MoveToEquipped(move),

            MoveDropZone.ZoneType.Available =>
                _service.MoveToAvailable(move),

            _ => false
        };

        if (!success)
        {
            _view.ResetDrag(move);
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
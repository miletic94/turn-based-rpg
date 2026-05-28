using System;
using System.Collections.Generic;

public class MoveManagementController
{
    private readonly MoveManagementView _view;
    private readonly MoveLoadoutService _service;
    private Action _onSave;
    private MoveManagementPresentation _presentation;


    public MoveManagementController(
        MoveManagementView view,
        MoveLoadoutService service,
        Action onSave)
    {
        _view = view;
        _service = service;
        _onSave = onSave;
    }

    public void Initialize(
        MoveManagementPresentation presentation)
    {
        _presentation = presentation;

        var availableSlots =
            _view.AvailablePanel.Render(presentation.AvailableSlots);
        var equippedSlots =
            _view.EquippedPanel.Render(presentation.EquippedSlots);

        BindSlots(availableSlots, MoveDropZoneType.Available);
        BindSlots(equippedSlots, MoveDropZoneType.Equipped);

        _view.SaveButton.Bind(HandleSaveClicked);

        _view.UnequipAllButton.Bind(HandleUnequipAllClicked);
    }

    private void HandleSaveClicked()
    {
        if (!_service.TrySave())
            _feedback.Publish(
    new FeedbackMessage(
        "Not enough equipped moves",
        FeedbackType.Warning));
        return;

        _onSave?.Invoke();
    }

    private void HandleUnequipAllClicked()
    {
        _service.UnequipAll();
        RefreshPanels();
    }

    private void RefreshPanels()
    {
        var availableSlots = _view.AvailablePanel.GetViews();
        var equippedSlots = _view.EquippedPanel.GetViews();

        var availableMoveIds = _service.AvailableMoves;
        var equippedMoveIds = _service.EquippedMoves;

        RefreshSlotGroup(availableSlots, availableMoveIds);
        RefreshSlotGroup(equippedSlots, equippedMoveIds);
    }

    private void RefreshSlotGroup(
    List<MoveSlotItemView> slots,
    List<int> moveIds)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < moveIds.Count)
            {
                var moveId = moveIds[i];
                slots[i].SetContent(_presentation.MoveItemDataById[moveId]);
            }
            else
            {
                slots[i].ClearContent();
            }
        }
    }

    private void BindSlots(
        List<MoveSlotItemView> slots,
        MoveDropZoneType zoneType)
    {
        foreach (var slot in slots)
        {
            slot.SetZoneType(zoneType);
            slot.Bind(HandleMoveDropRequested);
        }
    }

    private bool HandleMoveDropRequested(
        int moveId,
        MoveDropZoneType dropZone)
    {
        return dropZone switch
        {
            MoveDropZoneType.Equipped => _service.TryEquip(moveId),
            MoveDropZoneType.Available => _service.TryUnequip(moveId),
            _ => false
        };
    }
}
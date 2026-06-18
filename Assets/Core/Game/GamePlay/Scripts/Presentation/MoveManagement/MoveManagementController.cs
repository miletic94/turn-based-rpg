using System;
using System.Collections.Generic;

public class MoveManagementController
{
    private readonly MoveManagementView _view;
    private readonly MoveLoadoutService _service;
    private readonly UIFeedbackBus _uiFeedbackBus;
    private Action _onSave;
    private MoveManagementPresentation _presentation;
    private MoveDescriptionService _moveDescriptionService;


    public MoveManagementController(
        MoveManagementView view,
        MoveLoadoutService service,
        MoveDescriptionService moveDescriptionService,
        UIFeedbackBus uiFeedbackBus,
        Action onSave)
    {
        _view = view;
        _service = service;
        _moveDescriptionService = moveDescriptionService;
        _uiFeedbackBus = uiFeedbackBus;
        _onSave = onSave;
    }

    public void Initialize(
        MoveManagementPresentation presentation)
    {
        _presentation = presentation;

        RefreshPanels();

        _view.SaveButton.Bind(HandleSaveClicked);

        _view.UnequipAllButton.Bind(HandleUnequipAllClicked);
    }

    private void RefreshPanels()
    {
        var availableSlots =
            _view.AvailablePanel.Render(_presentation.AvailableSlots);
        var equippedSlots =
            _view.EquippedPanel.Render(_presentation.EquippedSlots);

        var availableMoveIds = _service.AvailableMoves;
        var equippedMoveIds = _service.EquippedMoves;

        RefreshSlotGroup(availableSlots, availableMoveIds);
        RefreshSlotGroup(equippedSlots, equippedMoveIds);

        BindSlots(availableSlots, MoveDropZoneType.Available);
        BindSlots(equippedSlots, MoveDropZoneType.Equipped);

        BindSlotItems(availableSlots);
        BindSlotItems(equippedSlots);
    }

    private void RefreshSlotGroup(
    List<MoveSlotItemView> slots,
    List<int> moveIds)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].ClearContent();

            if (i < moveIds.Count)
            {
                var moveId = moveIds[i];
                slots[i].SetContent(_presentation.MoveItemDataById[moveId]);
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
            slot.Bind(HandleMoveDropRequested, HandleMoveDropAllowed);
        }
    }

    private void BindSlotItems(List<MoveSlotItemView> slots)
    {
        foreach (var slot in slots)
        {
            var slotItem = slot.GetContentView();
            if (slotItem != null)
            {
                slotItem.BindHoverable(
                    onHoverDelayed: HandleHoverDelayed,
                    onHoverExited: HandleHoverExit);
            }
        }
    }

    private void HandleHoverDelayed(HoverData hoverData)
    {
        if (hoverData.Data is MoveHoverData moveHoverData)
        {
            _uiFeedbackBus.Publish(
                new MoveDescriptionTooltipMessage
                    (_moveDescriptionService.Describe(moveHoverData.Move),
                        moveHoverData.RectTransform));
        }
    }
    private void HandleHoverExit(HoverData hoverData)
    {
        _uiFeedbackBus.Publish(new HideMessage());
    }

    private bool HandleMoveDropRequested(
        int moveId,
        MoveDropZoneType dropZone)
    {
        return dropZone switch
        {
            MoveDropZoneType.Equipped => _service.CanEquip(moveId),
            MoveDropZoneType.Available => _service.CanUnequip(moveId),
            _ => false
        };
    }
    private void HandleMoveDropAllowed(int moveId, MoveDropZoneType dropZone)
    {
        switch (dropZone)
        {
            case MoveDropZoneType.Equipped:
                _service.Equip(moveId);
                break;
            case MoveDropZoneType.Available:
                _service.Unequip(moveId);
                break;
        }
    }

    private void HandleSaveClicked()
    {
        if (!_service.TrySave(out var errorMessage))
        {
            _uiFeedbackBus.Publish(new WarningMessage { Message = errorMessage });
            return;
        }

        _onSave?.Invoke();
    }

    private void HandleUnequipAllClicked()
    {
        _service.UnequipAll();
        RefreshPanels();
    }
}
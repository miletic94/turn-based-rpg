using System.Collections.Generic;

public class MMController
{
    private readonly MoveManagementPanel _availablePanel;
    private readonly MoveManagementPanel _equipedPpanel;
    // [SerializeField] private ClickableUI _uneqipAllButton;
    private readonly MoveLoadoutService _moveLoadoutService;
    private Dictionary<int, MoveItemData> _cachedMoveItemData;

    public MMController(
        MoveManagementPanel availableMovesPanel,
        MoveManagementPanel equippedMovesPanel,
         MoveLoadoutService moveLoadoutService)
    {
        _availablePanel = availableMovesPanel;
        _equipedPpanel = equippedMovesPanel;
        _moveLoadoutService = moveLoadoutService;
    }
    public void Initialize(
        List<MoveSlotData> availableMovesData,
        List<MoveSlotData> equippedMovesData,
        Dictionary<int, MoveItemData> moveItemDataById)
    {
        _cachedMoveItemData = moveItemDataById;
        var availableSlots = _availablePanel.Render(availableMovesData);
        var equippedSLots = _equipedPpanel.Render(equippedMovesData);
        foreach (var slot in availableSlots)
        {
            slot.SetZoneType(MoveDropZoneType.Available);
            slot.Bind(HandleMoveDropRequested);
        }
        foreach (var slot in equippedSLots)
        {
            slot.SetZoneType(MoveDropZoneType.Equipped);
            slot.Bind(HandleMoveDropRequested);
        }
    }

    private bool HandleMoveDropRequested(int moveId, MoveDropZoneType dropZone)
    {
        return dropZone switch
        {
            MoveDropZoneType.Equipped => _moveLoadoutService.TryEquip(moveId),
            MoveDropZoneType.Available => _moveLoadoutService.TryUnequip(moveId),
            _ => false
        };
    }
}


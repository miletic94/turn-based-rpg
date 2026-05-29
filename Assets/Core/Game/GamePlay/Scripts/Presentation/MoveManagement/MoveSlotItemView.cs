using System;
using UnityEngine;

public class MoveSlotItemView :
    SlotItemView<
        MoveSlotData,
        MoveManagementMoveItem,
        MoveItemData>,
    IDropZone
{
    public delegate bool MoveDropRequestedHandler(int moveId, MoveDropZoneType dropZoneType);
    private MoveDropRequestedHandler _moveDropRequested;
    public MoveDropZoneType ZoneType { get; private set; }
    public void SetZoneType(MoveDropZoneType zoneType)
    {
        ZoneType = zoneType;
    }

    // TODO: CanAccept shouldn't mutate state
    public bool CanAccept(DragContext context)
    {
        return
            context.Data is MoveDragData moveData
            && _moveDropRequested.Invoke(moveData.MoveId, ZoneType);

    }

    // TODO: Accept shouldn't change visuals. 
    // There should be only one source of truth and it should be in Controlelr/Service 
    public void Accept(DragContext context)
    {
        if (context.Data is MoveDragData moveData)
        {
            context.Draggable.transform.SetParent(transform);
            context.Draggable.transform.localPosition = Vector3.zero;
        }
    }
    public void Bind(MoveDropRequestedHandler moveDropRequested)
    {
        _moveDropRequested = moveDropRequested;
    }
}

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveDropZone : MonoBehaviour, IDropHandler
{
    public enum ZoneType
    {
        Available,
        Equipped
    }

    public ZoneType zoneType;

    public Action<MoveItemView, ZoneType> OnItemDropped;

    public void OnDrop(PointerEventData eventData)
    {
        var item =
            eventData.pointerDrag.GetComponent<MoveItemView>();

        if (item == null)
            return;

        OnItemDropped?.Invoke(
            item,
            zoneType);
    }
}
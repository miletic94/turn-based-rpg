using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneUI : MonoBehaviour, IDropHandler
{
    public event Action<DraggableUI, DropZoneUI> OnDropped;
    public void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<DraggableUI>();

        if (item?.Payload == null)
            return;

        OnDropped?.Invoke(item, this);
    }
}
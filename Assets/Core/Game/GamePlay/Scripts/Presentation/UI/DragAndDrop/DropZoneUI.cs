using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneUI : MonoBehaviour,
    IDropHandler
{
    private IDropZone _dropZone;

    private void Awake()
    {
        _dropZone = GetComponent<IDropZone>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggable =
                 eventData.pointerDrag.GetComponent<DraggableUI>();

        if (draggable == null)
            return;

        var context = draggable.GetContext();

        if (context == null)
            return;

        context.TargetZone = _dropZone;
        if (!_dropZone.CanAccept(context))
            return;

        _dropZone.Accept(context);
    }
}
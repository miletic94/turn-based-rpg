using System;
using TMPro;
using UnityEngine;

public class InteractableElementUI : MonoBehaviour
{
    [SerializeField] private TextLabelUI _label;
    public TextLabelUI Label => _label;
    [SerializeField] private ClickableUI _clickable;
    public ClickableUI Clickable => _clickable;
    [SerializeField] private HoverableUI _hoverable;
    public HoverableUI Hoverable => _hoverable;
    [SerializeField] private DraggableUI _draggable;
    public DraggableUI Draggable => _draggable;

    public void SetClickable(bool isClickable)
    {
        if (_clickable == null)
        {
            Debug.LogError("ClickableUI missing on gam object", gameObject);
        }
        _clickable.IsInteractable = isClickable;
    }
    public void SetHoverable(bool isHoverable)
    {
        if (_hoverable == null)
        {
            Debug.LogError("HoverableUI missing on gam object", gameObject);
        }
        _hoverable.IsInteractable = isHoverable;
    }
    public void SetDraggable(bool isDraggable)
    {
        if (_draggable == null)
        {
            Debug.LogError("DraggableUI missing on gam object", gameObject);
        }
        _draggable.IsInteractable = isDraggable;
    }

}
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

    public InteractableElementUI SetInteractable(bool isInteractable)
    {
        if (_clickable != null)
        {
            _clickable.IsInteractable = isInteractable;
        }
        if (_hoverable != null)
        {
            _hoverable.IsInteractable = isInteractable;
        }
        if (_draggable != null)
        {
            _draggable.IsInteractable = isInteractable;
        }
        return this;
    }
}
using System;
using UnityEngine;

public static class InteractebleElementExtensions
{
    public static InteractableElementUI SetLabel(
        this InteractableElementUI element,
        string text
        )
    {
        if (element.Label == null)
        {
            Debug.LogError("Missing LabelUI component on InteractableElementUI game object", element.gameObject);
        }
        element.Label.SetText(text);
        return element;
    }

    public static InteractableElementUI MakeClickable(
        this InteractableElementUI element,
        Action onClicked = null)
    {
        if (element.Clickable == null)
        {
            Debug.LogError("Missing ClickableUI component on InteractableElementUI game object", element.gameObject);
        }
        element.Clickable?.Bind(onClicked);
        element.SetClickable(true);
        return element;
    }

    public static InteractableElementUI MakeHoverable(
    this InteractableElementUI element,
    Action onHoverEntered = null,
    Action onHoverDelayed = null,
    Action onHoverExited = null)
    {
        if (element.Hoverable == null)
        {
            Debug.LogError("Missing HoverableUI component on InteractableElementUI game object", element.gameObject);
        }
        element.Hoverable?.Bind(onHoverEntered, onHoverDelayed, onHoverExited);

        element.SetHoverable(true);
        return element;
    }

    public static InteractableElementUI MaheDraggable(
        this InteractableElementUI element,
        IDraggablePayload payload,
        Action onDragStarted = null,
        Action onDragging = null,
        Action onDragEnded = null)
    {
        if (element.Draggable == null)
        {
            Debug.LogError("Missing Draggable component on InteractableElementUI game object", element.gameObject);
        }
        element.Draggable?.SetPayload(payload);
        element.Draggable?.Bind(onDragStarted, onDragging, onDragEnded);
        element.SetDraggable(true);
        return element;
    }
}
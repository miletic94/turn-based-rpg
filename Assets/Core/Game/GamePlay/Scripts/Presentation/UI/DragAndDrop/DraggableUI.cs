using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DraggableUI : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    public IDraggablePayload Payload { get; private set; }

    private Transform _originalParent;
    private Canvas _canvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    private DragContext _context;

    public event Action DragBegan;
    public event Action Dragging;
    public event Action DragEnded;
    private bool _isInteractable = true;
    public void SetInteractable(bool isInteractable)
    {
        _isInteractable = isInteractable;
    }

    public void OnEnable()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

    public void Bind(
        Action onDragBegan = null,
        Action onDragging = null,
        Action onDragEnded = null
        )
    {
        DragBegan = onDragBegan;
        Dragging = onDragging;
        DragEnded = onDragEnded;
    }

    public void SetPayload(IDraggablePayload payload)
    {
        Payload = payload;
    }

    public void ReturnToOriginalParent()
    {
        transform.SetParent(_originalParent);
        transform.localPosition = Vector3.zero;
    }

    public DragContext GetContext()
    {
        return _context;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_isInteractable) return;

        _originalParent = transform.parent;

        var sourceZone =
            GetComponentInParent<IDropZone>();

        var dataSource =
            GetComponent<IDragDataSource>();

        _context = new DragContext
        {
            Draggable = this,
            Data = dataSource?.GetDragData(),
            SourceZone = sourceZone
        };

        transform.SetParent(_canvas.transform);

        _canvasGroup.blocksRaycasts = false;

        InvokeDragBegan();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isInteractable) return;

        transform.position =
            eventData.position;

        InvokeDragging(); // TODO: Pay attention - this will happen multiple times during dragging
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isInteractable) return;

        _canvasGroup.blocksRaycasts = true;

        if (transform.parent == _canvas.transform)
        {
            ReturnToOriginalParent();
        }

        InvokeDragEnded();
    }

    private void InvokeDragBegan()
    {
        if (_isInteractable)
        {
            DragBegan?.Invoke();
        }
    }

    private void InvokeDragging()
    {
        if (_isInteractable)
        {
            Dragging?.Invoke();
        }
    }

    private void InvokeDragEnded()
    {
        if (_isInteractable)
        {
            DragEnded?.Invoke();
        }
    }

}
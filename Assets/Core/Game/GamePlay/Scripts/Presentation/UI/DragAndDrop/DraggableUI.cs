using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DraggableUI : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private Transform _originalParent;
    private Canvas _canvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    private DragContext _context;

    private bool _isInteractable = true;
    public void SetInteractable(bool isInteractable)
    {
        _isInteractable = isInteractable;
    }

    public void OnEnable()
    {
        _canvas = GetComponentInParent<Canvas>();
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isInteractable) return;

        transform.position =
            eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isInteractable) return;

        _canvasGroup.blocksRaycasts = true;

        if (transform.parent == _canvas.transform)
        {
            ReturnToOriginalParent();
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveItemView : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    public Move MoveData { get; private set; }

    private Transform _originalParent;
    private Canvas _canvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text Label;

    public void Initialize(
        Move move,
        Canvas canvas)
    {
        MoveData = move;
        _canvas = canvas;

        _canvasGroup =
            GetComponent<CanvasGroup>();

        if (_canvasGroup == null)
        {
            _canvasGroup =
                gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalParent = transform.parent;

        transform.SetParent(
            _canvas.transform);

        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position =
            eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;

        if (transform.parent == _canvas.transform)
        {
            ReturnToOriginalParent();
        }
    }

    public void ReturnToOriginalParent()
    {
        transform.SetParent(
            _originalParent);

        transform.localPosition =
            Vector3.zero;
    }
}
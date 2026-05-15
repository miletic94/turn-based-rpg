using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveManagementView : MonoBehaviour
{
    public event Action<MovePayload, MoveDropZoneType> OnMoveDropped;

    [Header("Zones")]
    [SerializeField] private MoveDropZone _availableZone;
    [SerializeField] private MoveDropZone _equippedZone;

    [Header("Prefab")]
    [SerializeField] private MoveItemView _moveItemPrefab;

    [Header("Controls")]
    [SerializeField] private Button _saveButton;

    public event Action SaveClicked;
    private readonly Dictionary<Move, MoveItemView> _items =
        new Dictionary<Move, MoveItemView>();

    private void OnEnable()
    {
        _availableZone.OnDropped += HandleDrop;
        _equippedZone.OnDropped += HandleDrop;
        _saveButton.onClick.AddListener(HandleSave);
    }

    private void OnDisable()
    {
        _availableZone.OnDropped -= HandleDrop;
        _equippedZone.OnDropped -= HandleDrop;
        _saveButton.onClick.RemoveListener(HandleSave);
    }

    public void Render(
        List<Move> available,
        List<Move> equipped)
    {
        RenderMoves(_availableZone.transform, available);
        RenderMoves(_equippedZone.transform, equipped);
    }

    private void RenderMoves(
        Transform container,
        List<Move> moves)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        foreach (var move in moves)
        {
            var moveItemView = Instantiate(
                _moveItemPrefab,
                container);

            moveItemView
                .SetLabel(move.Name)
                .MakeHoverable()
                .MaheDraggable(new MovePayload(move));

            _items[move] = moveItemView;
        }
    }


    public void HandleDrop(
        DraggableUI item,
        DropZoneUI zone)
    {
        var moveDropZone = zone.GetComponent<MoveDropZone>();

        if (moveDropZone == null) return;
        if (item.Payload is MovePayload movePayload)
        {
            item.transform.SetParent(zone.transform);
            item.transform.localPosition = Vector3.zero;

            OnMoveDropped?.Invoke(movePayload, moveDropZone.Type);
        }
    }

    public void ResetDrag(Move move)
    {
        if (_items.TryGetValue(move, out var item))
        {
            item.Draggable.ReturnToOriginalParent();
        }
    }

    public void HandleSave()
    {
        SaveClicked?.Invoke();
    }
}
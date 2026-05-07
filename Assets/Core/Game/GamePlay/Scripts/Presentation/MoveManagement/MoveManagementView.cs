using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveManagementView : MonoBehaviour, IMoveManagementView
{
    [Header("Containers")]
    [SerializeField] private Transform _availableContainer;
    [SerializeField] private Transform _equippedContainer;

    [Header("Zones")]
    [SerializeField] private MoveDropZone _availableZone;
    [SerializeField] private MoveDropZone _equippedZone;

    [Header("Prefab")]
    [SerializeField] private MoveItemView _moveItemPrefab;

    [Header("Canvas")]
    [SerializeField] private Canvas _canvas;

    [Header("Controls")]
    [SerializeField] private Button _saveButton;

    public event Action<Move, MoveDropZone.ZoneType> MoveDropped;
    public event Action SaveClicked;
    private readonly Dictionary<Move, MoveItemView> _items =
        new Dictionary<Move, MoveItemView>();

    private void OnEnable()
    {
        _availableZone.OnItemDropped += HandleDrop;
        _equippedZone.OnItemDropped += HandleDrop;
        _saveButton.onClick.AddListener(HandleSave);
    }

    private void OnDisable()
    {
        _availableZone.OnItemDropped -= HandleDrop;
        _equippedZone.OnItemDropped -= HandleDrop;
        _saveButton.onClick.RemoveListener(HandleSave);
    }

    public void HandleDrop(
        MoveItemView item,
        MoveDropZone.ZoneType zone)
    {
        Transform targetContainer =
    zone == MoveDropZone.ZoneType.Available
        ? _availableContainer
        : _equippedContainer;

        item.transform.SetParent(targetContainer);
        item.transform.localPosition = Vector3.zero;
        MoveDropped?.Invoke(item.MoveData, zone);
    }

    public void HandleSave()
    {
        SaveClicked?.Invoke();
    }

    public void Render(
        List<Move> available,
        List<Move> equipped)
    {
        RenderMoves(_availableContainer, available);
        RenderMoves(_equippedContainer, equipped);
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

            var text =
                moveItemView.GetComponentInChildren<TMP_Text>();

            text.text = move.Name;

            moveItemView.Initialize(
                move,
                _canvas);

            _items[move] = moveItemView;
        }
    }

    public void ResetDrag(Move move)
    {
        if (_items.TryGetValue(move, out var item))
        {
            item.ReturnToOriginalParent();
        }
    }
}
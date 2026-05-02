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

    private Action _onSave;

    public void Show(
        List<Move> availableMoves,
        List<Move> equippedMoves)
    {
        RenderMoves(
            _availableContainer,
            availableMoves);

        RenderMoves(
            _equippedContainer,
            equippedMoves);
    }

    public void BindDropZones(
        Action<MoveItemView, MoveDropZone.ZoneType> onDropped)
    {
        _availableZone.OnItemDropped = onDropped;
        _equippedZone.OnItemDropped = onDropped;
    }

    public void BindSave(Action onSave)
    {
        _onSave = onSave;

        _saveButton.onClick.RemoveAllListeners();
        _saveButton.onClick.AddListener(
            HandleSave);
    }

    public void Unbind()
    {
        _availableZone.OnItemDropped = null;
        _equippedZone.OnItemDropped = null;

        _onSave = null;

        _saveButton.onClick.RemoveAllListeners();
    }

    private void HandleSave()
    {
        _onSave?.Invoke();
    }

    private void RenderMoves(
        Transform container,
        List<Move> moves)
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);

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
        }
    }
}
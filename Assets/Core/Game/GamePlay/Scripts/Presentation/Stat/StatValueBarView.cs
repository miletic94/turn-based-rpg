
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SlotVisualConfig
{
    public StatValueBarView.SlotType Type;
    public Color Color;
}

public class StatValueBarView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private StatValueBarSlotUI _statValueBarSlotPrefab;
    [SerializeField] private List<SlotVisualConfig> _slotVisualConfig;

    private Dictionary<SlotType, Color> _colorBySlotType;
    public void CreateColorPallete()
    {
        _colorBySlotType = new();
        foreach (var colorBySlotType in _slotVisualConfig)
        {
            _colorBySlotType.Add(colorBySlotType.Type, colorBySlotType.Color);
        }
    }

    public void ShowValueBar(int baseValue, int currentValue, int capValue)
    {
        if (_colorBySlotType == null) CreateColorPallete();
        Clear();

        int difference = currentValue - baseValue;

        bool isBuffed = difference > 0;

        int visibleBase = Math.Min(baseValue, currentValue);

        int modifiedAmount = Math.Abs(difference);

        int emptyAmount = capValue - visibleBase - modifiedAmount;
        Debug.Log($"_colorBySlotType == null {_colorBySlotType == null}");

        var modifiedColor = isBuffed
            ? _colorBySlotType[SlotType.Buff]
            : _colorBySlotType[SlotType.Debuff];

        CreateSlotSegment(_colorBySlotType[SlotType.Base], visibleBase);
        CreateSlotSegment(modifiedColor, modifiedAmount);
        CreateSlotSegment(_colorBySlotType[SlotType.Empty], emptyAmount);
    }
    // TODO: Pooling
    private void CreateSlotSegment(Color color, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var slot = Instantiate(_statValueBarSlotPrefab, _container);
            slot.SetColor(color);
        }
    }

    private void Clear()
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
    }

    public enum SlotType
    {
        Empty,
        Base,
        Buff,
        Debuff
    }
}
using System.Collections.Generic;
using UnityEngine;

public class StatView : MonoBehaviour
{
    [SerializeField] private StatHeaderView _statHeaderPrefab;
    [SerializeField] private StatRowView _statRowPrefab;
    [SerializeField] private Transform _container;

    private Dictionary<StatType, StatRowView> _rows = new();

    public void ShowStat(Combatant character)
    {
        var header = Instantiate(_statHeaderPrefab, _container);
        header.SetText(character.Name);

        foreach (var stat in character.GetStats())
        {
            var row = Instantiate(_statRowPrefab, _container);
            row.SetIdentifier(stat.type.ToString());
            row.SetValue(stat.currentValue.ToString());

            _rows[stat.type] = row;
        }
    }

    public void UpdateStat(Combatant character)
    {
        foreach (var stat in character.GetStats())
        {
            if (_rows.TryGetValue(stat.type, out var row))
            {
                row.SetValue(stat.currentValue.ToString());

                var comparison = stat.currentValue.CompareTo(stat.baseValue);
                row.SetColor(
                    comparison > 0 ? Color.green :
                    comparison < 0 ? Color.red :
                    Color.white
                );


            }
        }
    }
}
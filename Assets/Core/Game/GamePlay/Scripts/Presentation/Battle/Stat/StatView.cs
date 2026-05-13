using System.Collections.Generic;
using UnityEngine;

public class StatView : MonoBehaviour
{
    [SerializeField] private StatHeaderView _statHeaderPrefab;
    [SerializeField] private StatRowView _statRowPrefab;
    [SerializeField] private Transform _container;
    private StatRowView _healthRow;

    private Dictionary<StatType, StatRowView> _rows = new();

    public void ShowStat(Combatant combatant)
    {
        var header = Instantiate(_statHeaderPrefab, _container);
        header.SetText(combatant.Name);

        // TODO: Handle Health better
        _healthRow = Instantiate(_statRowPrefab, _container);
        _healthRow.SetIdentifier("Health");
        _healthRow.SetValue(combatant.Health.ToString());


        foreach (var stat in combatant.GetStats())
        {
            var row = Instantiate(_statRowPrefab, _container);
            row.SetIdentifier(stat.Type.ToString());
            row.SetValue(stat.GetCurrentValue().ToString());

            _rows[stat.Type] = row;
        }
    }

    public void UpdateHealth(Combatant character)
    {
        _healthRow.SetValue(character.Health.ToString());
    }
    public void UpdateStats(Combatant character)
    {
        foreach (var stat in character.GetStats())
        {
            if (_rows.TryGetValue(stat.Type, out var row))
            {
                row.SetValue(stat.GetCurrentValue().ToString());

                var comparison = stat.GetCurrentValue().CompareTo(stat.BaseValue);
                row.SetColor(
                    comparison > 0 ? Color.green :
                    comparison < 0 ? Color.red :
                    Color.white
                );
            }
        }
    }
}
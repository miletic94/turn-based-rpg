using UnityEngine;

public class StatsManagementView : MonoBehaviour
{
    [SerializeField] private StatAvailablePointsView _statAvailablePointsViewPrefab;
    [SerializeField] private StatItemRowView _statItemRowViewPrefab;
    [SerializeField] private Transform _statManagementContainer;


    public void SetAvailablePoints(int availablePoints)
    {
        _statAvailablePointsViewPrefab.SetAvailablePoints(availablePoints);
    }
    public void ShowAvailablePoints(int availablePoints)
    {
        var view = Instantiate(_statAvailablePointsViewPrefab, _statManagementContainer);
        view.SetAvailablePoints(availablePoints);
    }
    public StatItemRowView ShowStatItemRow(string statName, string statValue)
    {
        var row = Instantiate(_statItemRowViewPrefab, _statManagementContainer);
        row.SetStatName(statName);
        row.SetStatValue(statValue);
        return row;
    }
}
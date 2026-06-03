using TMPro;
using UnityEngine;
namespace Presentation.StatsManagement.StatsPanel
{
    public class StatsPanelView
        : ListView<StatRowView, StatRowViewData>
    {
        [SerializeField] private TMP_Text _availablePointsText;
        public void ShowAvailablePoints(string availablePoints)
        {
            _availablePointsText.text = availablePoints;
        }
    }
}
using UnityEngine;
namespace Presentation.StatsManagement.StatsPanel
{
    public class StatsPanelView
        : ListView<StatRowView, StatRowViewData>
    {
        [SerializeField] private TMPro.TextMeshProUGUI _availablePointsText;
        public void ShowAvailablePoints(string availablePoints)
        {
            _availablePointsText.text = availablePoints;
        }
    }
}
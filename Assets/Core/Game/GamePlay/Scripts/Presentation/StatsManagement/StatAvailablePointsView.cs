using TMPro;
using UnityEngine;

public class StatAvailablePointsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _availablePointsValueText;

    public void SetAvailablePoints(int availablePoints)
    {
        _availablePointsValueText.text = $"{availablePoints}";
    }
}
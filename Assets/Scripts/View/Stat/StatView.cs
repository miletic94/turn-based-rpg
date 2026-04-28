using UnityEngine;

public class StatView : MonoBehaviour
{
    [SerializeField] private StatHeaderView _statHeaderPrefab;
    [SerializeField] private StatRowView _statRowPrefab;
    [SerializeField] private Transform _container;

    public void ShowStat(Character character)
    {
        var statsHeaderView = Instantiate(_statHeaderPrefab, transform);
        statsHeaderView.SetText(character.Name);

        foreach (var stat in character.GetStats())
        {
            var statRowPrefab = Instantiate(_statRowPrefab, transform);
            statRowPrefab.SetIdentifier(stat.type.ToString());
            statRowPrefab.SetValue(stat.currentValue.ToString());
        }
    }
}
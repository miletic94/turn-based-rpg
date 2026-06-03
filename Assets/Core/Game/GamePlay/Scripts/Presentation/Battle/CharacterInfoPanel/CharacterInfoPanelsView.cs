using System.Collections.Generic;
using Presentation.Stat;
using UnityEngine;

public class CharacterInfoPanelsView : MonoBehaviour
{
    [SerializeField] private CharacterInfoPanelView _playerPanelView;
    [SerializeField] private CharacterInfoPanelView _enemyPanelView;

    public void CreatePanels(CharacterInfoPanelData playerData, CharacterInfoPanelData enemyData)
    {
        CreatePlayerPanel(playerData);
        CreateEnemyPanel(enemyData);
    }
    public void SetPlayerHealthBar(float healthNormalized)
    {
        _playerPanelView.SetHealthBar(healthNormalized);
    }
    public void SetEnemyHealthBar(float healthNormalized)
    {
        _enemyPanelView.SetHealthBar(healthNormalized);
    }
    public void RefreshPlayerStatsPanel(List<StatItemData> statItems)
    {
        _playerPanelView.RefreshStats(statItems);
    }
    public void RefreshEnemyStatsPanel(List<StatItemData> statItems)
    {
        _enemyPanelView.RefreshStats(statItems);
    }
    private void CreatePlayerPanel(CharacterInfoPanelData data)
    {
        _playerPanelView.CreatePanel(data);
    }
    private void CreateEnemyPanel(CharacterInfoPanelData data)
    {
        _enemyPanelView.CreatePanel(data);
    }
}
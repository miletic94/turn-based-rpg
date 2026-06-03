using System.Collections.Generic;
using Presentation.Stat;
using UnityEngine;

public class CharacterInfoPanelView : MonoBehaviour
{
    [SerializeField] CharacterPortraitView _portraitView;
    [SerializeField] HealthBarView _healthBarView;
    [SerializeField] BattleStatsPanelView _statsView;

    public void CreatePanel(CharacterInfoPanelData data)
    {
        _portraitView.SetPortraitSprite(data.Portrait);
        _healthBarView.SetImmediate(data.HealthPercentage);
        _statsView.Render(data.Stats);
    }

    public void SetHealthBar(float healthPercentage)
    {
        _healthBarView.SetImmediate(healthPercentage);
    }
    public void RefreshStats(List<StatItemData> data)
    {
        _statsView.Refresh(data);
    }
}
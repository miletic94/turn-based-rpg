using System.Collections.Generic;
using Presentation.Stat;
using UnityEngine;

public class CharacterInfoPanelData
{
    public Sprite Portrait { get; private set; }
    public float HealthPercentage { get; private set; }
    public List<StatItemData> Stats { get; private set; }

    public CharacterInfoPanelData(Sprite portrait, float healthPercentage, List<StatItemData> stats)
    {
        Portrait = portrait;
        HealthPercentage = healthPercentage;
        Stats = stats;
    }
}
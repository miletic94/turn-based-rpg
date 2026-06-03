using System;
using UnityEngine;

public class RewardBootstrapper : MonoBehaviour
{
    [SerializeField] Screen RewardScreen;
    [SerializeField] Background _rewardBackground;
    [SerializeField] RewardListView _rewardListView;
    private RewardController _rewardController;

    public void Load(Character enemy, Action<Move> onRewardSelected)
    {
        _rewardController = new RewardController(_rewardListView, onRewardSelected);
        _ = _rewardController.Initialize(enemy);
        RewardScreen.Show();
        _rewardBackground.Show();
    }
    public void Unload()
    {
        RewardScreen.Hide();
        _rewardBackground.Hide();
    }
}
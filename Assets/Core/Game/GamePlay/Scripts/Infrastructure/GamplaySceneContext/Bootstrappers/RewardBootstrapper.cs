using System;
using UnityEngine;

public class RewardBootstrapper : MonoBehaviour
{
    [SerializeField] Screen RewardScreen;
    [SerializeField] RewardListView _rewardListView;
    private RewardController _rewardController;

    public void Load(Character enemy, Action<Move> onRewardSelected)
    {
        _rewardController = new RewardController(_rewardListView, new MoveDescriptionService(), onRewardSelected);
        _rewardController.Initialize(enemy);
        RewardScreen.Show();
    }
    public void Unload()
    {
        RewardScreen.Hide();
    }
}
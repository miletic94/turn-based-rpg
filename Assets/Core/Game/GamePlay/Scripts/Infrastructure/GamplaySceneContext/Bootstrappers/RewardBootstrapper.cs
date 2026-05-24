using System;
using UnityEngine;

public class RewardBootstrapper : MonoBehaviour
{
    [SerializeField] Screen RewardScreen;
    [SerializeField] RewardView _rewardView;
    private RewardController _rewardController;

    public void Load(Character enemy, Action<Move> onRewardSelected)
    {
        var rewardService = new RewardService(enemy.Moves);
        _rewardController = new RewardController(_rewardView, new MoveDescriptionService(), rewardService, onRewardSelected);
        _rewardController.Initialize();
        RewardScreen.Show();
    }
    public void Unload()
    {
        RewardScreen.Hide();
    }
}
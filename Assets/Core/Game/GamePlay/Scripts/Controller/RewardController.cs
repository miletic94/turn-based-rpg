using System;
using UnityEngine;

public class RewardController
{
    private readonly RewardView _rewardView;
    private readonly RewardService _rewardService;
    private readonly Action<Move> _onRewardSelected;

    public RewardController(RewardView rewardView,
    RewardService rewardService,
    Action<Move> onRewardSelected)
    {
        _rewardView = rewardView;
        _rewardService = rewardService;
        _onRewardSelected = onRewardSelected;
    }
    public void Initialize()
    {
        ShowRewards();
    }

    private void ShowRewards()
    {
        _rewardView.ShowRewards(_rewardService.GetRewards(), HandleRewardSelected);
    }

    public void HandleRewardSelected(Move move)
    {
        _onRewardSelected?.Invoke(move);
    }
}
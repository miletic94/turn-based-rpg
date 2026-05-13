using System;

public class RewardController
{
    private readonly RewardView _rewardView;
    private readonly MoveDescriptionService _moveDescriptionService;
    private readonly RewardService _rewardService;
    private readonly Action<Move> _onRewardSelected;

    public RewardController(RewardView rewardView,
    MoveDescriptionService moveDescriptionService,
    RewardService rewardService,
    Action<Move> onRewardSelected)
    {
        _rewardView = rewardView;
        _moveDescriptionService = moveDescriptionService;
        _rewardService = rewardService;
        _onRewardSelected = onRewardSelected;
    }
    public void Initialize()
    {
        ShowRewards();
    }

    private void ShowRewards()
    {
        _rewardView.ShowRewards(_rewardService.GetRewards(), HandleRewardSelected, HandleRewardHovered);
    }

    public void HandleRewardSelected(Move move)
    {
        _onRewardSelected?.Invoke(move);
    }
    public void HandleRewardHovered(Move move)
    {
        UnityEngine.Debug.Log(_moveDescriptionService.Describe(move));
    }
}
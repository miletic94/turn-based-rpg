using System;
using System.Collections.Generic;

public class RewardController
{
    private readonly RewardView _rewardView;
    private readonly MoveDescriptionService _moveDescriptionService;
    private readonly Action<Move> _onRewardSelected;

    public RewardController(RewardView rewardView,
    MoveDescriptionService moveDescriptionService,
    Action<Move> onRewardSelected)
    {
        _rewardView = rewardView;
        _moveDescriptionService = moveDescriptionService;
        _onRewardSelected = onRewardSelected;
    }
    public void Initialize(Character enemy)
    {
        var rewardDataList = CreateRewardItemData(enemy);

        _rewardView.ShowRewards(rewardDataList);

        foreach (var rewardData in rewardDataList)
        {
            var view = _rewardView.GetView(rewardData.Id);
            var move = enemy.Moves.Find(move => move.Id == rewardData.Id);
            view.BindClick(() => HandleRewardSelected(move));
        }
    }

    public List<RewardItemData> CreateRewardItemData(Character enemy)
    {
        var data = new List<RewardItemData>();
        foreach (var move in enemy.Moves)
        {
            data.Add(new RewardItemData(move.Id, null));
        }
        return data;
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


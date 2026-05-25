using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;


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
    public async void Initialize(Character enemy)
    {
        var rewardDataList = await CreateRewardItemData(enemy);

        _rewardView.ShowRewards(rewardDataList);

        foreach (var rewardData in rewardDataList)
        {
            var view = _rewardView.GetView(rewardData.Id);
            var move = enemy.Moves.Find(move => move.Id == rewardData.Id);
            view.BindClick(() => HandleRewardSelected(move));
        }
    }

    public async Awaitable<List<RewardItemData>> CreateRewardItemData(Character enemy)
    {
        var tasks = enemy.Moves.Select(async move =>
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(move.IconAddress);
            var sprite = await handle.Task;

            return new RewardItemData(move.Id, sprite);
        });

        return (await Task.WhenAll(tasks)).ToList();
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


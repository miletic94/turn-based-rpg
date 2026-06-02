using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class RewardController
{
    private readonly RewardListView _rewardListView;
    private readonly Action<Move> _onRewardSelected;

    public RewardController(RewardListView rewardListView,
    Action<Move> onRewardSelected)
    {
        _rewardListView = rewardListView;
        _onRewardSelected = onRewardSelected;
    }
    public async Awaitable Initialize(Character enemy)
    {
        var rewardDataList = await CreateRewardItemData(enemy);

        _rewardListView.Render(rewardDataList);

        foreach (var rewardData in rewardDataList)
        {
            var view = _rewardListView.GetView(rewardData.Id);
            var move = enemy.Moves.Find(move => move.Id == rewardData.Id);
            view.BindClick(() => HandleRewardSelected(move));
        }
    }

    public async Awaitable<List<MoveItemData>> CreateRewardItemData(Character enemy)
    {
        var tasks = enemy.Moves.Select(async move =>
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(move.IconAddress);
            var sprite = await handle.Task;

            return new MoveItemData(move.Id, sprite);
        });

        return (await Task.WhenAll(tasks)).ToList();
    }
    public void HandleRewardSelected(Move move)
    {
        _onRewardSelected?.Invoke(move);
    }
}


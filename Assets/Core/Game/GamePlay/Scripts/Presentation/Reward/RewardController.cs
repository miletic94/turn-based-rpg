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
    private readonly UIFeedbackBus _uiFeedbackBus;
    private readonly MoveDescriptionService _moveDescriptionService;

    public RewardController(
        RewardListView rewardListView,
        UIFeedbackBus uiFeedbackBus,
        MoveDescriptionService moveDescriptionService,
        Action<Move> onRewardSelected)
    {
        _rewardListView = rewardListView;
        _uiFeedbackBus = uiFeedbackBus;
        _moveDescriptionService = moveDescriptionService;
        _onRewardSelected = onRewardSelected;
    }
    public async Awaitable Initialize(Character enemy)
    {
        var rewardDataList = await CreateRewardItemData(enemy);

        var views = _rewardListView.Render(rewardDataList);

        foreach (var view in views)
        {
            view.BindClick(_onRewardSelected);
            view.BindHoverable(HandleHoverDelayed, HandleHoverExit);
        }
    }

    public async Awaitable<List<MoveItemData>> CreateRewardItemData(Character enemy)
    {
        var tasks = enemy.Moves.Select(async move =>
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(move.IconAddress);
            var sprite = await handle.Task;

            return new MoveItemData(move.Id, move, sprite);
        });

        return (await Task.WhenAll(tasks)).ToList();
    }

    private void HandleHoverDelayed(HoverData hoverData)
    {
        if (hoverData.Data is MoveHoverData moveHoverData)
        {
            _uiFeedbackBus.Publish(
                new MoveDescriptionTooltipMessage
                    (_moveDescriptionService.Describe(moveHoverData.MoveId),
                        moveHoverData.RectTransform));
        }
    }
    private void HandleHoverExit(HoverData hoverData)
    {
        _uiFeedbackBus.Publish(new HideMessage());
    }
}


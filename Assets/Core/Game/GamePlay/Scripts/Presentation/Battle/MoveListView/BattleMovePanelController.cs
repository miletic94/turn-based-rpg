using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BattleMovePanelController
{
    private readonly BattleMoveListView _moveListView;
    private readonly UIFeedbackBus _uiFeedbackBus;
    private readonly MoveDescriptionService _moveDescriptionService;

    public BattleMovePanelController(BattleMoveListView moveListView, UIFeedbackBus uiFeedbackBus, MoveDescriptionService moveDescriptionService)
    {
        _moveListView = moveListView;
        _uiFeedbackBus = uiFeedbackBus;
        _moveDescriptionService = moveDescriptionService;
    }

    public async Awaitable Initialize(
        List<Move> moves,
        Action<Move> handleMoveSelected)
    {
        var moveDataList = await CreateMoveItemData(moves);

        var views = _moveListView.Render(moveDataList);

        foreach (var moveData in moveDataList)
        {
            var view = _moveListView.GetView(moveData.Id);
            var move = moves.Find(move => move.Id == moveData.Id);

            view.BindClick(() => handleMoveSelected(move));
            view.BindHoverable(HandleHoverDelayed, HandleHoverExit);
        }
    }
    private async Awaitable<List<BattleMoveItemData>> CreateMoveItemData(List<Move> moves)
    {
        var tasks = moves.Select(async move =>
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(move.IconAddress);
            var sprite = await handle.Task;

            return new BattleMoveItemData(move.Id, sprite);

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
        else
        {
            Debug.LogError("Unexpected hover data type: " + hoverData.Data.GetType());
        }
    }
    private void HandleHoverExit(HoverData hoverData)
    {
        _uiFeedbackBus.Publish(new HideMessage());
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BattleMovePanelController
{
    private readonly BattleMovePanelView _battleMoveView;
    private readonly MoveTooltipBinder _moveTooltipBinder;

    public BattleMovePanelController(BattleMovePanelView battleView, MoveTooltipBinder moveTooltipBinder)
    {
        _battleMoveView = battleView;
        _moveTooltipBinder = moveTooltipBinder;
    }

    public async Awaitable Initialize(
        List<Move> moves,
        Action<Move> handleMoveSelected)
    {
        var moveDataList = await CreateMoveItemData(moves);

        _battleMoveView.ShowMoves(moveDataList);

        foreach (var rewardData in moveDataList)
        {
            var view = _battleMoveView.GetView(rewardData.Id);
            var move = moves.Find(move => move.Id == rewardData.Id);

            view.BindClick(() => handleMoveSelected(move));
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
}
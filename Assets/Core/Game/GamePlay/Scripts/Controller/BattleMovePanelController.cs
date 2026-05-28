using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BattleMovePanelController
{
    private readonly BattleMoveListView _moveListView;

    public BattleMovePanelController(BattleMoveListView moveListView)
    {
        _moveListView = moveListView;
    }

    public async Awaitable Initialize(
        List<Move> moves,
        Action<Move> handleMoveSelected)
    {
        var moveDataList = await CreateMoveItemData(moves);

        _moveListView.Render(moveDataList);

        foreach (var moveData in moveDataList)
        {
            var view = _moveListView.GetView(moveData.Id);
            var move = moves.Find(move => move.Id == moveData.Id);

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
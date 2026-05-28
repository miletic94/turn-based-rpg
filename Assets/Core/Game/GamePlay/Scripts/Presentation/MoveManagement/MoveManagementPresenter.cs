using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MoveManagementPresenter
{
    public async Awaitable<MoveManagementPresentation> Build(
        List<Move> availableMoves,
        List<Move> equippedMoves,
        int maxEquipped)
    {
        var allMoves =
            availableMoves.Concat(equippedMoves).ToList();

        var itemDataById =
            await CreateMoveItemData(allMoves);

        return new MoveManagementPresentation
        {
            MoveItemDataById = itemDataById,

            AvailableSlots =
                CreateSlots(
                    availableMoves,
                    itemDataById,
                    availableMoves.Count + equippedMoves.Count),

            EquippedSlots =
                CreateSlots(
                    equippedMoves,
                    itemDataById,
                    maxEquipped)
        };
    }

    private async Awaitable<Dictionary<int, MoveItemData>>
        CreateMoveItemData(List<Move> moves)
    {
        var tasks = moves.Select(async move =>
        {
            var handle =
                Addressables.LoadAssetAsync<Sprite>(
                    move.IconAddress);

            var sprite =
                await handle.Task;

            return new MoveItemData(
                move.Id,
                sprite);
        });

        var results =
            await Task.WhenAll(tasks);

        return results.ToDictionary(x => x.Id);
    }

    private List<MoveSlotData> CreateSlots(
        List<Move> moves,
        Dictionary<int, MoveItemData> itemDataById,
        int slotCount)
    {
        var slots = new List<MoveSlotData>();

        for (int i = 0; i < slotCount; i++)
        {
            MoveItemData content = null;

            if (i < moves.Count)
            {
                content =
                    itemDataById[moves[i].Id];
            }

            slots.Add(
                new MoveSlotData(
                    i,
                    content));
        }

        return slots;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class MMController : MonoBehaviour
{
    [SerializeField] private MoveManagementPanel _availablePanel;
    [SerializeField] private MoveManagementPanel _equipedPpanel;
    [SerializeField] private ClickableUI _uneqipAllButton;

    private MoveLoadoutService _moveLoadoutService;
    private MoveLoadout _moveLoadout;

    private async void Awake()
    {
        try
        {
            await Initialize(
                new List<Move>
                {
                    new Move(4, "Shadow Bolt", "bolt-spell-cast", new Cost(), new List<IMoveEffect>()),
                },
                new List<Move>
                {
                    new Move(0, "Slash", "blade-drag", new Cost(), new List<IMoveEffect>()),
                    new Move(1, "Shield Up", "shield", new Cost(), new List<IMoveEffect>()),
                    new Move(2, "Battle Cry", "trumpet-flag", new Cost(), new List<IMoveEffect>()),
                    new Move(3, "Second Wind", "healing", new Cost(), new List<IMoveEffect>())
                }
            );
        }
        catch
        {
            throw;
        }
    }

    // public MMController(MoveManagementPanel panel)
    // {
    //     _panel = panel;
    // }
    public async Awaitable Initialize(List<Move> availableMoves, List<Move> equippedMoves)
    {
        _moveLoadout = new MoveLoadout
        {
            AvailableMoves = availableMoves.Select(move => move.Id).ToHashSet(),
            EquippedMoves = equippedMoves.Select(move => move.Id).ToHashSet()
        };
        _moveLoadoutService = new MoveLoadoutService(_moveLoadout);

        var availalbePanelSlotsData = await CreateMoveSlotData(availableMoves, availableMoves.Count + equippedMoves.Count);
        var equippedPanelSlotsData = await CreateMoveSlotData(equippedMoves, _moveLoadout.MaxEquipped);

        var availableSlots = _availablePanel.Render(availalbePanelSlotsData);
        var equippedSLots = _equipedPpanel.Render(equippedPanelSlotsData);
        foreach (var slot in availableSlots)
        {
            slot.SetZoneType(MoveDropZoneType.Available);
            slot.Bind(HandleMoveDropRequested);
        }
        foreach (var slot in equippedSLots)
        {
            slot.SetZoneType(MoveDropZoneType.Equipped);
            slot.Bind(HandleMoveDropRequested);
        }
    }

    private bool HandleMoveDropRequested(int moveId, MoveDropZoneType dropZone)
    {
        return dropZone switch
        {
            MoveDropZoneType.Equipped => _moveLoadoutService.TryEquip(moveId),
            MoveDropZoneType.Available => _moveLoadoutService.TryUnequip(moveId),
            _ => false
        };
    }
    public async Awaitable<List<MoveSlotData>>
        CreateMoveSlotData(List<Move> moves, int slotCount)
    {
        var tasks = Enumerable
            .Range(0, slotCount)
            .Select(async slotIndex =>
            {
                MoveItemData content = null;

                if (slotIndex < moves.Count)
                {
                    var move = moves[slotIndex];

                    var handle =
                        Addressables.LoadAssetAsync<Sprite>(
                            move.IconAddress);

                    var sprite =
                        await handle.Task;

                    content = new MoveItemData(
                        move.Id,
                        sprite);
                }

                return new MoveSlotData(
                    slotIndex,
                    content);
            });

        return (await Task.WhenAll(tasks)).ToList();
    }
}


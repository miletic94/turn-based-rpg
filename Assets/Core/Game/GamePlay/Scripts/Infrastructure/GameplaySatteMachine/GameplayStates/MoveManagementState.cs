using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MoveManagementState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;


    public MoveManagementState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;
    }

    public async void Enter()
    {
        var hero = _context.GameplayContext.Hero;
        var moveLoadout = new MoveLoadout
        {
            AvailableMoves = hero.AvailableMoves.Select(move => move.Id).ToHashSet(),
            EquippedMoves = hero.EquippedMoves.Select(move => move.Id).ToHashSet()
        };
        var moveLoadoutService = new MoveLoadoutService(moveLoadout);

        var movesItemData = await CreateMoveItemData(hero.AvailableMoves.Concat(hero.EquippedMoves).ToList());
        var availableMoveSlots = CreateMoveSlotData(
            moveLoadout.AvailableMoves.Select(id => movesItemData[id]).ToList(),
            hero.AvailableMoves.Count + hero.EquippedMoves.Count);

        var equippedMoveSlots = CreateMoveSlotData(
            moveLoadout.EquippedMoves.Select(id => movesItemData[id]).ToList(),
            moveLoadout.MaxEquipped);

        _context.MoveManagementBootstrapper.Load(
            moveLoadoutService,
            OnSave
        );
        _context.MoveManagementBootstrapper.Initialize(
            availableMoveSlots,
            equippedMoveSlots,
            movesItemData);

    }

    public void OnSave(List<Move> availableMoves, List<Move> equippedMoves)
    {
        _context.GameplayContext.Hero.SetAvailableMoves(availableMoves);
        _context.GameplayContext.Hero.SetEquippedMoves(equippedMoves);
        _gameplayStateMachine.EnterMap();
    }

    public void Exit()
    {
        // _context.MoveManagementBootstrapper.Unload();
    }

    public async Awaitable<Dictionary<int, MoveItemData>>
       CreateMoveItemData(List<Move> moves)
    {
        var tasks = moves.Select(async move =>
        {
            var handle =
                Addressables.LoadAssetAsync<Sprite>(
                    move.IconAddress);

            var sprite = await handle.Task;

            return new MoveItemData(
                move.Id,
                sprite);
        });

        var results = await Task.WhenAll(tasks);

        return results.ToDictionary(x => x.Id);
    }

    public List<MoveSlotData> CreateMoveSlotData(List<MoveItemData> moveItemData, int slotCount)
    {
        List<MoveSlotData> slotList = new();

        for (int slotIndex = 0; slotIndex < slotCount; slotIndex++)
        {
            MoveItemData content = null;
            if (slotIndex < moveItemData.Count)
            {
                content = moveItemData[slotIndex];
            }
            slotList.Add(new MoveSlotData(
                slotIndex,
                content));
        }
        return slotList;
    }
}
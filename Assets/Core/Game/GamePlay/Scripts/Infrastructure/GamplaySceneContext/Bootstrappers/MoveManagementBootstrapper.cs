using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveManagementBootstrapper : MonoBehaviour
{
    [SerializeField]
    private Screen _moveManagementScreen;

    [SerializeField]
    private MoveManagementPanel _availableMovesPanel;
    [SerializeField]
    private MoveManagementPanel _equippedMovesPanel;

    private MMController _moveManagementController;

    public void Load(
        MoveLoadoutService moveLoadoutService,
        Action<List<Move>, List<Move>> onSave)
    {
        _moveManagementController =
        new MMController(
            _availableMovesPanel,
            _equippedMovesPanel,
            moveLoadoutService);
    }

    public void Initialize(
        List<MoveSlotData> availableMovesData,
        List<MoveSlotData> equippedMovesData,
        Dictionary<int, MoveItemData> moveItemDataById
    )
    {
        _moveManagementController.Initialize(availableMovesData, equippedMovesData, moveItemDataById);
    }

    // public void Unload()
    // {
    //     _moveManagementScreen.Hide();
    //     _moveManagementController.Unbind();
    // }
}
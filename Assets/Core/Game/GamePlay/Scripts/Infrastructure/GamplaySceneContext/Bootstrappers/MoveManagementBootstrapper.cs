using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveManagementBootstrapper : MonoBehaviour
{
    [SerializeField]
    private MoveManagementScreen _moveManagementScreen;

    [SerializeField]
    private MoveManagementView _moveManagementView;

    private MoveManagementController _moveManagementController;

    public void Load(
        Hero hero,
        Action<List<Move>, List<Move>> onSave)
    {
        var loadout = CreateLoadout(hero);
        var service = new MoveLoadoutService(loadout);

        _moveManagementController = new MoveManagementController(
            _moveManagementView,
            service,
            onSave);

        _moveManagementController.Bind();

        _moveManagementScreen.Show();
    }

    public void Unload()
    {
        _moveManagementScreen.Hide();
        _moveManagementController.Unbind();
    }

    MoveLoadout CreateLoadout(Hero hero)
    {
        var loadout = new MoveLoadout
        {
            AvailableMoves = new List<Move>(hero.AvailableMoves),
            EquippedMoves = new List<Move>(hero.EquippedMoves),
            MaxEquipped = 4
        };

        return loadout;
    }
}
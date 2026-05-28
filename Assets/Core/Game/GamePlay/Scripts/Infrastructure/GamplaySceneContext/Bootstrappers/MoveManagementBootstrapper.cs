using System;
using UnityEngine;

public class MoveManagementBootstrapper : MonoBehaviour
{
    [SerializeField]
    private MoveManagementView _screenView;
    [SerializeField] private Screen _moveManagementScreen;

    private MoveManagementController _controller;

    public void Load(
        MoveLoadoutService service,
        MoveManagementPresentation presentation,
        Action onSave)
    {
        _controller =
            new MoveManagementController(
                _screenView,
                service,
                onSave);

        _controller.Initialize(
            presentation);

        _moveManagementScreen.Show();
    }

    public void Unload()
    {
        _moveManagementScreen.Hide();
    }
}
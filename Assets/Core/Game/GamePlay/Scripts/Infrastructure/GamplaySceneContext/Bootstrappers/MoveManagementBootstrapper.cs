using System;
using UnityEngine;

public class MoveManagementBootstrapper : MonoBehaviour
{
    [SerializeField]
    private MoveManagementView _screenView;
    [SerializeField] private Screen _moveManagementScreen;
    [SerializeField] private Background _moveManagementBackground;
    private MoveManagementController _controller;

    public void Load(
        MoveLoadoutService service,
        MoveDescriptionService moveDescriptionService,
        MoveManagementPresentation presentation,
        UIFeedbackBus uiFeedbackBus,
        Action onSave)
    {
        _controller =
            new MoveManagementController(
                _screenView,
                service,
                moveDescriptionService,
                uiFeedbackBus,
                onSave);

        _controller.Initialize(
            presentation);

        _moveManagementScreen.Show();
        _moveManagementBackground.Show();
    }

    public void Unload()
    {
        _moveManagementScreen.Hide();
        _moveManagementBackground.Hide();
    }
}
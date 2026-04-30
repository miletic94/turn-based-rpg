using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] Button _startGameButton;
    [SerializeField] Button _exitButton;

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(OnStartGameClicked);
        _exitButton.onClick.AddListener(OnExitClicked);
    }

    public async void OnStartGameClicked()
    {
        await AppContext.ApplicationStateMachine.EnterGameplay();
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
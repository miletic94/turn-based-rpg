using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : IAsyncState
{
    private readonly ApplicationStateMachine _appStateMachine;
    public MainMenuState(ApplicationStateMachine appStateMachine)
    {
        _appStateMachine = appStateMachine;
    }

    public async Awaitable Enter()
    {
        await SceneManager.LoadSceneAsync("MainMenuScene");
    }

    public Awaitable Exit()
    {
        return AwaitableUtils.CompletedTask;
    }
}
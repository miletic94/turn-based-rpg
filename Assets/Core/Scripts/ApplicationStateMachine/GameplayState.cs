using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayState : IAsyncState
{
    private readonly ApplicationStateMachine _appStateMachine;
    private GameplayStateMachine _gameplayStateMachine;

    public GameplayState(ApplicationStateMachine appStateMachine)
    {
        _appStateMachine = appStateMachine;
    }

    public async Awaitable Enter()
    {
        await SceneManager.LoadSceneAsync("GameplayScene");

        var gameplaySceneContext = Object.FindFirstObjectByType<GameplaySceneContext>();

        _gameplayStateMachine = new GameplayStateMachine(gameplaySceneContext);

        _gameplayStateMachine.EnterMap();
    }

    public Awaitable Exit()
    {
        return default;
    }
}
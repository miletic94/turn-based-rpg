using UnityEngine;

public class AppBootstrapper : MonoBehaviour
{
    private ApplicationStateMachine _appStateMachine;
    // private SceneLoaderService _sceneLoader;

    private async void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // _sceneLoader = new SceneLoaderService();

        _appStateMachine =
            new ApplicationStateMachine();

        AppContext.Initialize(_appStateMachine);

        await _appStateMachine.EnterMainMenu();
    }
}
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

        await _appStateMachine.EnterMainMenu();
    }
}
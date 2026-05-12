using UnityEngine;

public class AppBootstrapper : MonoBehaviour
{
    // private SceneLoaderService _sceneLoader;

    private async void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // _sceneLoader = new SceneLoaderService();
        var appStateMachine = new ApplicationStateMachine();


        AppContext.Initialize(appStateMachine);

        await appStateMachine.EnterMainMenu();
    }
}
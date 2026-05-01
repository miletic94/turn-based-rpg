using UnityEngine;

public class AppBootstrapper : MonoBehaviour
{
    // private SceneLoaderService _sceneLoader;

    private async void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // _sceneLoader = new SceneLoaderService();
        var eventBus = new EventBus();
        var appStateMachine = new ApplicationStateMachine();


        AppContext.Initialize(eventBus, appStateMachine);

        await appStateMachine.EnterMainMenu();
    }
}
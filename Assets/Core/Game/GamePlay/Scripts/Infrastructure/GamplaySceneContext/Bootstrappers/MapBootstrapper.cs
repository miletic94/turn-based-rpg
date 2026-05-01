using System.Collections.Generic;
using UnityEngine;

public class MapBootstrapper : MonoBehaviour
{
    [SerializeField] MapScreen MapScreen;
    [SerializeField] LevelTreeView _levelTreeView;
    private LevelTreeViewBinder _levelTreeViewBinder;

    public void InitializeAndRun(GameplayStateMachine stateMachine)
    {
        var characters = LoadCharacters();
        var player = characters[0];
        var enemy = characters[1];


        _levelTreeViewBinder = new LevelTreeViewBinder(
            _levelTreeView,
            stateMachine,
            player,
            new LevelProvider(new List<Character> { enemy }));
        _levelTreeViewBinder.Bind();

        MapScreen.Show();
    }

    private List<Character> LoadCharacters()
    {
        return new CharacterDeserializer().Deserialize();
    }

    public void Unload()
    {
        _levelTreeViewBinder.Unbind();
        MapScreen.Hide();
    }
}
using System.Collections.Generic;
using System.Linq;
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

        _levelTreeViewBinder = new LevelTreeViewBinder(
            _levelTreeView,
            stateMachine,
            player,
            new LevelProvider(characters.Skip(1).ToList()));
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
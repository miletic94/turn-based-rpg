using UnityEngine;
public class CombatantViewFactory : MonoBehaviour
{
    [SerializeField] private CombatantView playerPrefab;
    [SerializeField] private CombatantView enemyPrefab;

    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform enemySpawn;

    public CombatantView CreateView(Character character)
    {
        CombatantView prefab;
        Transform spawn;

        if (character.Role == CombatantRole.Player)
        {
            prefab = playerPrefab;
            spawn = playerSpawn;
            prefab.FlipSpriteX(true);
        }
        else
        {
            prefab = enemyPrefab;
            spawn = enemySpawn;
        }

        var view = Instantiate(prefab, spawn.position, Quaternion.identity, spawn);

        return view;
    }
}
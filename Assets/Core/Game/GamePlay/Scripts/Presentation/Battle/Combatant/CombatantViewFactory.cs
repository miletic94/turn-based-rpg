using System.Collections.Generic;
using UnityEngine;
public class CombatantViewFactory : MonoBehaviour
{
    [SerializeField] private CombatantPrefabDatabase database;
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform enemySpawn;

    private void Awake()
    {
        database.Init();
    }

    public CombatantView CreateView(Combatant combatant)
    {
        var prefab = database.Get(StringUtils.ToNoSpaceLowercase(combatant.Name));

        if (prefab == null)
        {
            Debug.LogError($"No prefab found for {combatant.Name}");
            return null;
        }

        Transform spawn = combatant.Role == CombatantRole.Player
            ? playerSpawn
            : enemySpawn;

        var view = Instantiate(prefab, spawn.position, Quaternion.identity, spawn);

        if (combatant.Role == CombatantRole.Player)
            view.FlipSpriteX(true);

        return view;
    }
}
using System.Collections.Generic;
using UnityEngine;
public class CombatantViewFactory : MonoBehaviour
{
    [SerializeField] private CombatantView prefab;
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform enemySpawn;

    public CombatantView CreateView(Combatant combatant)
    {
        Transform spawn = combatant.Role == CombatantRole.Player
            ? playerSpawn
            : enemySpawn;

        var view = Instantiate(prefab, spawn.position, Quaternion.identity, spawn);
        view.SetSprite(combatant.Portrait);

        if (combatant.Role == CombatantRole.Player)
            view.FlipSpriteX(true);

        return view;
    }
}
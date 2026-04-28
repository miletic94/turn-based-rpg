using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneBootstrapper : MonoBehaviour
{
    [SerializeField] MoveView _moveView;

    private async void Start()
    {
        var combatantFactory = new CombatantFactory(_moveView);

        var characters = new CharacterDeserializer().Deserialize();
        var knight = characters[0];
        combatantFactory.CreatePlayer(knight);

        var witch = characters[1];
        combatantFactory.CreateEnemy(witch);

        BattleState battleState = new BattleState(new List<Character>() { knight, witch });
        MoveExecutor moveExecutor = new MoveExecutor();
        BattleController battleController = new BattleController(battleState, moveExecutor);

        _moveView.SetMoves(knight.Moves);

        try
        {
            await battleController.BattleLoop();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

}
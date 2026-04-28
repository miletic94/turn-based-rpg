using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneBootstrapper : MonoBehaviour
{
    [SerializeField] MoveView _moveView;
    [SerializeField] CombatantViewFactory _combatantViewFactory;
    [SerializeField] StatView _statView;

    private async void Start()
    {
        var combatantFactory = new CombatantFactory(_moveView);
        var eventBus = new BattleEventBus();

        var characters = new CharacterDeserializer().Deserialize();

        var knight = characters[0];
        combatantFactory.CreatePlayer(knight);
        _combatantViewFactory.CreateView(knight, eventBus);
        _statView.ShowStat(knight);


        var witch = characters[1];
        combatantFactory.CreateEnemy(witch);
        _combatantViewFactory.CreateView(witch, eventBus);

        BattleState battleState = new BattleState(new List<Character>() { knight, witch });
        MoveExecutor moveExecutor = new MoveExecutor();
        BattleController battleController = new BattleController(battleState, moveExecutor, eventBus);

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
using System.Collections.Generic;
using UnityEngine;

public class BattleController
{
    private ICommandFactory _commandFactory;
    private CombatantController _combatantController;
    private MoveController _moveController;
    private StatController _statController;
    public BattleController(ICommandFactory commandFactory, CombatantController combatantController, MoveController moveController, StatController statController)
    {
        _commandFactory = commandFactory;
        _combatantController = combatantController;
        _moveController = moveController;
        _statController = statController;
    }
    public async Awaitable Run()
    {
        var characters = new CharacterDeserializer().Deserialize();

        var knight = characters[0];
        var witch = characters[1];

        // SetupCharacters
        knight.Role = CombatantRole.Player;
        witch.Role = CombatantRole.Enemy;

        knight.MoveProvider = new PlayerBattleMoveProvider(_moveController);
        witch.MoveProvider = new AIBattleMoveProvider();

        _combatantController.Create(knight);
        _combatantController.Create(witch);

        var battleState = new BattleState(new List<Character> { knight, witch });
        var moveExecutor = new MoveExecutor();

        var battleExecutor = new BattleExecutor(_commandFactory, battleState, moveExecutor);

        _moveController.ShowMoves(knight.Moves);
        _statController.Show(knight);

        await battleExecutor.BattleLoop();
    }
}
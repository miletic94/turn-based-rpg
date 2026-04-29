using System.Collections.Generic;
using System.Diagnostics;

public class MoveExecutedCommand : BaseCommand, ICommandVoid
{
    private MoveExecutedCommandData _commandData;
    private StatController _statController;
    private CombatantController _combatantController;
    public MoveExecutedCommand SetData(MoveExecutedCommandData commandData)
    {
        _commandData = commandData;
        return this;
    }
    public override void ResolveDependencies()
    {
        _statController = _diContainer.Resolve<StatController>();
        _combatantController = _diContainer.Resolve<CombatantController>();
    }

    public void Execute()
    {
        _statController.Update(_commandData.Player);
        _combatantController.UpdateAll(new List<Character> { _commandData.Player, _commandData.Enemy });
    }

}
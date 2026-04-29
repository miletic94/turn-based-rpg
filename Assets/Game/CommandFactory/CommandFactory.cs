public class CommandFactory : ICommandFactory
{
    private readonly IDiContainer _diContainer;

    public CommandFactory(IDiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public TCommand CreateCommandVoid<TCommand>() where TCommand : ICommandVoid, new()
    {
        var command = new TCommand();
        command.SetObjectResolver(_diContainer);
        command.ResolveDependencies();
        return command;
    }
}
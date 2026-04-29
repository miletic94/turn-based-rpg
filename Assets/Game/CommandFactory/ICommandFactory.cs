public interface ICommandFactory
{
    TCommand CreateCommandVoid<TCommand>() where TCommand : ICommandVoid, new();
}
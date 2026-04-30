public abstract class BaseCommand : IBaseCommand
{
    protected IDiContainer _diContainer;

    public void SetObjectResolver(IDiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public abstract void ResolveDependencies();
}
public interface IBaseCommand
{
    void SetObjectResolver(IDiContainer diContainer);
    void ResolveDependencies();
}
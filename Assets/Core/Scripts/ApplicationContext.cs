public static class AppContext
{
    public static ApplicationStateMachine ApplicationStateMachine { get; private set; }

    public static void Initialize(
        ApplicationStateMachine applicationStateMachine)
    {
        ApplicationStateMachine = applicationStateMachine;
    }
}
public static class AppContext
{
    public static ApplicationStateMachine ApplicationStateMachine { get; private set; }
    public static EventBus EventBus { get; private set; }

    public static void Initialize(EventBus eventBus, ApplicationStateMachine applicationStateMachine)
    {
        ApplicationStateMachine = applicationStateMachine;
        EventBus = eventBus;
    }
}
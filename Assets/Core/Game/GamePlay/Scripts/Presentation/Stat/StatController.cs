
public class StatViewBinder
{
    private readonly StatView _view;
    private readonly IEventBus _bus;

    private Character _currentCharacter;

    public StatViewBinder(IEventBus bus, StatView view)
    {
        _bus = bus;
        _view = view;
    }

    public void Bind()
    {
        _bus.Subscribe<MoveExecutedEvent>(OnMoveExecuted);
    }

    public void Unbind()
    {
        _bus.Unsubscribe<MoveExecutedEvent>(OnMoveExecuted);
    }

    public void Show(Character character)
    {
        _currentCharacter = character;
        _view.ShowStat(character);
    }

    private void OnMoveExecuted(MoveExecutedEvent e)
    {
        // only update if relevant character changed
        if (e.Source == _currentCharacter)
            Refresh(e.Source);

        if (e.Target == _currentCharacter)
            Refresh(e.Target);
    }

    private void Refresh(Character character)
    {
        _view.UpdateStat(character);
    }
}
public class MoveViewBinder
{
    private readonly IMoveView _view;
    private readonly IEventBus _bus;

    private Character _currentActor;

    public MoveViewBinder(IEventBus bus, IMoveView view)
    {
        _bus = bus;
        _view = view;
    }

    public void Bind()
    {
        _bus.Subscribe<MoveSelectionRequestedEvent>(OnSelectionRequested);
    }

    public void Unbind()
    {
        _bus.Unsubscribe<MoveSelectionRequestedEvent>(OnSelectionRequested);
    }

    private void OnSelectionRequested(MoveSelectionRequestedEvent e)
    {
        _currentActor = e.Actor;

        _view.ShowMoves(e.Actor.Moves);

        _view.EnableInput(OnMoveSelected);
    }

    private void OnMoveSelected(Move move)
    {
        _view.DisableInput();

        _bus.Publish(new MoveSelectedEvent(_currentActor, move));
    }
}
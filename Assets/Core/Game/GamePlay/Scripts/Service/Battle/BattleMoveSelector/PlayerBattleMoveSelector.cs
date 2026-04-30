using UnityEngine;

public class PlayerBattleMoveSelector : IBattleMoveSelector
{
    private readonly IEventBus _bus;

    public PlayerBattleMoveSelector(EventBus bus)
    {
        _bus = bus;
    }

    public Awaitable<Move> SelectMoveAsync(Character actor, BattleData state)
    {
        _bus.Publish(new MoveSelectionRequestedEvent(actor));

        return WaitForSelection(actor);
    }

    private Awaitable<Move> WaitForSelection(Character actor)
    {
        var tcs = new AwaitableCompletionSource<Move>();

        void Handler(MoveSelectedEvent e)
        {
            if (e.Actor != actor) return;

            _bus.Unsubscribe<MoveSelectedEvent>(Handler);
            tcs.SetResult(e.Move);
        }

        _bus.Subscribe<MoveSelectedEvent>(Handler);

        return tcs.Awaitable;
    }
}
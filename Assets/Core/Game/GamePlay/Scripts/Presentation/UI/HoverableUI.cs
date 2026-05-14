using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverableUI : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField]
    private float _hoverDelay = 1f;

    public event Action OnHoverEntered;
    public event Action OnHoverExited;
    public event Action OnHoverDelayed;

    private CancellationTokenSource _hoverCts;

    public void OnPointerEnter(
        PointerEventData eventData)
    {
        OnHoverEntered?.Invoke();

        _hoverCts?.Cancel();
        _hoverCts = new CancellationTokenSource();

        HandleDelayedHoverAsync(
            _hoverCts.Token);
    }

    public void OnPointerExit(
        PointerEventData eventData)
    {
        _hoverCts?.Cancel();

        OnHoverExited?.Invoke();
    }

    private async Awaitable HandleDelayedHoverAsync(
        CancellationToken token)
    {
        try
        {
            await Awaitable.WaitForSecondsAsync(
                _hoverDelay,
                cancellationToken: token);

            if (!token.IsCancellationRequested)
            {
                OnHoverDelayed?.Invoke();
            }
        }
        catch (OperationCanceledException)
        {
            // ignored
        }
    }

    private void OnDsiable()
    {
        _hoverCts?.Cancel();
        _hoverCts?.Dispose();
    }
}
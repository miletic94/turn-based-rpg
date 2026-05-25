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

    public event Action HoverEntered;
    public event Action HoverExited;
    public event Action HoverDelayed;

    private bool _isInteractable = true;
    public void SetInteractable(bool isInteractable)
    {
        _isInteractable = isInteractable;
    }

    private CancellationTokenSource _hoverCts;

    private void OnDsiable()
    {
        _hoverCts?.Cancel();
        _hoverCts?.Dispose();
    }

    public void Bind(
        Action onHoverEntered = null,
        Action onHoverDelayed = null,
        Action onHoverExited = null)
    {
        HoverEntered = onHoverEntered;
        HoverDelayed = onHoverDelayed;
        HoverExited = onHoverExited;
    }
    private void InvokeHoverEntered()
    {
        if (_isInteractable)
        {
            HoverEntered?.Invoke();
        }
    }
    private void InvokeHoverDelayed()
    {
        if (_isInteractable)
        {
            HoverDelayed?.Invoke();
        }
    }
    private void InvokeHoverExited()
    {
        if (_isInteractable)
        {
            HoverExited?.Invoke();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isInteractable) return;

        InvokeHoverEntered();

        _hoverCts?.Cancel();
        _hoverCts = new CancellationTokenSource();

        HandleDelayedHoverAsync(_hoverCts.Token);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isInteractable) return;

        _hoverCts?.Cancel();

        InvokeHoverExited();
    }

    private async Awaitable HandleDelayedHoverAsync(CancellationToken token)
    {
        if (!_isInteractable) return;

        try
        {
            await Awaitable.WaitForSecondsAsync(
                _hoverDelay,
                cancellationToken: token);

            if (!token.IsCancellationRequested)
            {
                InvokeHoverDelayed();
            }
        }
        catch (OperationCanceledException)
        {
            // ignored
        }
    }
}
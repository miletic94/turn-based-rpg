using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum HoverPhase
{
    Enter,
    Delayed,
    Exit
}

public readonly struct HoverData
{
    public HoverableUI Hoverable { get; }

    public HoverPhase Phase { get; }

    public object Data { get; }

    public HoverData(
        HoverableUI hoverable,
        HoverPhase phase,
        object data)
    {
        Hoverable = hoverable;
        Phase = phase;
        Data = data;
    }
}

public class HoverableUI :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [SerializeField]
    private float _hoverDelay = 1f;
    [SerializeField]
    private bool _disappearsOnClick = true;

    private bool _isInteractable = true;

    private CancellationTokenSource _hoverCts;

    private IHoverEnteredDataSource _hoverEnterDataSource;

    private IHoverDelayedDataSource _hoverDelayedDataSource;

    private IHoverExitedDataSource _hoverExitDataSource;

    public event Action<HoverData> HoverEntered;

    public event Action<HoverData> HoverDelayed;

    public event Action<HoverData> HoverExited;
    private bool _isHovered;

    private void Awake()
    {
        _hoverEnterDataSource =
            GetComponent<IHoverEnteredDataSource>();

        _hoverDelayedDataSource =
            GetComponent<IHoverDelayedDataSource>();

        _hoverExitDataSource =
            GetComponent<IHoverExitedDataSource>();
    }

    public void SetInteractable(bool isInteractable)
    {
        _isInteractable = isInteractable;

        if (!_isInteractable)
        {
            CancelHover();
        }
    }

    public void OnPointerEnter(
        PointerEventData eventData)
    {
        if (!_isInteractable)
            return;

        _isHovered = true;

        InvokeHoverEntered();

        BeginDelayedHover();
    }

    public void OnPointerExit(
        PointerEventData eventData)
    {
        if (!_isInteractable)
            return;
        EndHover();
    }

    private void BeginDelayedHover()
    {
        CancelHover();

        _hoverCts =
            new CancellationTokenSource();

        _ = HandleDelayedHoverAsync(_hoverCts.Token);
    }

    private async Awaitable HandleDelayedHoverAsync(
        CancellationToken token)
    {
        try
        {
            await Awaitable.WaitForSecondsAsync(
                _hoverDelay,
                cancellationToken: token);

            if (token.IsCancellationRequested)
                return;

            if (!_isInteractable)
                return;

            InvokeHoverDelayed();
        }
        catch (OperationCanceledException)
        {
            // ignored
        }
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (!_isInteractable)
            return;
        if (_isHovered && _disappearsOnClick && pointerData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("HoverableUI: Clicked while hovered, ending hover.");
            EndHover();
        }
    }

    private void InvokeHoverEntered()
    {
        var hoverData =
            new HoverData(
                this,
                HoverPhase.Enter,
                _hoverEnterDataSource?.GetHoverEnteredData());

        HoverEntered?.Invoke(hoverData);
    }

    private void InvokeHoverDelayed()
    {
        var hoverData =
            new HoverData(
                this,
                HoverPhase.Delayed,
                _hoverDelayedDataSource?.GetHoverDelayedData());
        HoverDelayed?.Invoke(hoverData);
    }

    private void InvokeHoverExited()
    {
        Debug.Log("InvokeHoverExited");

        var hoverData =
            new HoverData(
                this,
                HoverPhase.Exit,
                _hoverExitDataSource?.GetHoverExitedData());
        HoverExited?.Invoke(hoverData);
    }

    private void CancelHover()
    {
        _hoverCts?.Cancel();
        _hoverCts?.Dispose();
        _hoverCts = null;
    }

    private void OnDisable()
    {
        EndHover();
    }

    private void OnDestroy()
    {
        EndHover();
    }

    private void EndHover()
    {
        if (!_isHovered)
            return;

        _isHovered = false;

        CancelHover();
        InvokeHoverExited();
    }
}
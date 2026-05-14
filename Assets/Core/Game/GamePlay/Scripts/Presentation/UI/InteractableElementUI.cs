using System;
using TMPro;
using UnityEngine;

public class InteractebleElementUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private ClickableUI _clickable;
    [SerializeField] private HoverableUI _hoverable;
    private bool _isInteractable = true;

    public InteractebleElementUI SetText(string text)
    {
        _label.text = text;
        return this;
    }

    public InteractebleElementUI SetInteractable(bool isInteractable)
    {
        _isInteractable = isInteractable;
        return this;
    }

    public InteractebleElementUI OnClicked(Action onClicked)
    {
        _clickable.OnClicked += () =>
        {
            if (_isInteractable)
                onClicked();
        };
        return this;
    }

    public InteractebleElementUI OnHoverEntered(Action onHoverEntered)
    {
        _hoverable.OnHoverEntered += () =>
        {
            if (_isInteractable)
                onHoverEntered();
        };
        return this;
    }

    public InteractebleElementUI OnHoverDelayed(Action onHoverDelayed)
    {
        _hoverable.OnHoverDelayed += () =>
        {
            if (_isInteractable)
                onHoverDelayed();
        };
        return this;
    }

    public InteractebleElementUI OnHoverExited(Action onHoverExited)
    {
        _hoverable.OnHoverExited += () =>
        {
            if (_isInteractable)
                onHoverExited();
        };
        return this;
    }
}
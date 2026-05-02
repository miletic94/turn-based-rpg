using System;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : MonoBehaviour
{
    [SerializeField] Button movesManagementButton;
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetOnMoveManagementButtonClicked(Action onMoveManagementButtonClicked)
    {
        movesManagementButton.onClick.RemoveAllListeners();
        movesManagementButton.onClick.AddListener(() => onMoveManagementButtonClicked?.Invoke());
    }
}
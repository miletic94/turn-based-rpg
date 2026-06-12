using UnityEngine;
using UnityEngine.UI;

public class VerticalScrollRectInit : MonoBehaviour
{
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] Scrollbar _scrollbar;

    private void Start()
    {
        _scrollRect.verticalNormalizedPosition = 0f;
        _scrollbar.value = 0f;
    }
}
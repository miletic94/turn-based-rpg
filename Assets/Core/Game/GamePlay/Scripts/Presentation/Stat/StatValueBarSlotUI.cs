using UnityEngine;
using UnityEngine.UI;
public class StatValueBarSlotUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetColor(Color color)
    {
        color.a = 1;
        _image.color = color;
    }
}
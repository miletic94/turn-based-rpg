using UnityEngine;
using UnityEngine.UI;
public class MoveView1 : MonoBehaviour
{
    [SerializeField] Image _icon;

    public void SetSprite(Sprite sprite)
    {
        _icon.sprite = sprite;
    }
}
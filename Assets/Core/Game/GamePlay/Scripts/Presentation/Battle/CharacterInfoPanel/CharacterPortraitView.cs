using UnityEngine;
using UnityEngine.UI;

public class CharacterPortraitView : MonoBehaviour
{
    [SerializeField] private Image _portrait;
    public void SetPortraitSprite(Sprite sprite)
    {
        _portrait.sprite = sprite;
    }
}
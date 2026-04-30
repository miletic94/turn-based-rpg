using TMPro;
using UnityEngine;

public class StatHeaderView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textHolder;

    public void SetText(string text)
    {
        _textHolder.text = text;
    }
}
using TMPro;
using UnityEngine;

public class StatRowView : MonoBehaviour
{
    [SerializeField] TMP_Text _identifierTextHolder;
    [SerializeField] TMP_Text _valueTextHolder;
    public void SetIdentifier(string text)
    {
        _identifierTextHolder.text = text;
    }
    public void SetValue(string text)
    {
        _valueTextHolder.text = text;
    }
}
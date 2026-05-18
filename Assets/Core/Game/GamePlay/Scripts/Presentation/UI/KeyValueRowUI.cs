using UnityEngine;
public class KeyValueRowUI : MonoBehaviour
{
    [SerializeField] TextLabelUI _key;
    public TextLabelUI Key => _key;
    [SerializeField] TextLabelUI _value;
    public TextLabelUI Value => _value;
}
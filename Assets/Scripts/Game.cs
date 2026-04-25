using UnityEngine;

public class Game : MonoBehaviour
{
    private void Start()
    {
        new CharacterDeserializer().Deserialize();
    }
}
using UnityEngine;

public class MapBootstrapper : MonoBehaviour
{
    [SerializeField] MapScreen MapScreen;

    public void Show()
    {
        MapScreen.Show();
    }
    public void Hide()
    {
        MapScreen.Hide();
    }
}
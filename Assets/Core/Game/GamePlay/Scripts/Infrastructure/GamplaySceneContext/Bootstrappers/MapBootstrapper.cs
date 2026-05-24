using UnityEngine;

public class MapBootstrapper : MonoBehaviour
{
    [SerializeField] private Screen _mapScreen;
    [SerializeField] private MapView _mapView;
    public MapController Load()
    {
        _mapScreen.Show();

        return new MapController(_mapView);
    }

    public void Unload()
    {
        _mapScreen.Hide();
    }
}
using UnityEngine;

public class XpBootstrapper : MonoBehaviour
{
    [SerializeField] private Screen _xpScreen;
    [SerializeField] private XpView _xpView;
    public XpController Load(Xp xp)
    {
        _xpScreen.Show();
        var xpService = new XpService(xp);
        return new XpController(xpService, _xpView);
    }

    public void Unload()
    {
        _xpScreen.Hide();
    }
}
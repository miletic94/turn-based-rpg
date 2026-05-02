using UnityEngine;

public class MoveManagementBootstrapper : MonoBehaviour
{
    [SerializeField]
    private MoveManagementScreen _screen;

    [SerializeField]
    private MoveManagementView _view;

    private MoveManagementViewBinder _binder;

    public void Initialize(
        MoveLoadout loadout,
        MoveLoadoutService service,
        System.Action onSave)
    {
        _binder =
            new MoveManagementViewBinder(
                _view,
                loadout,
                service);

        _binder.Bind(
            onSave);

        _screen.gameObject.SetActive(true);
    }

    public void Unload()
    {
        _binder?.Unbind();

        _screen.gameObject.SetActive(false);
    }
}
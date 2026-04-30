using UnityEngine;

public class MoveManagementBootstrapper : MonoBehaviour
{
    [SerializeField] MoveManagementScreen MoveManagementScreen;
    public void Show()
    {
        MoveManagementScreen.Show();
    }
    public void Hide()
    {
        MoveManagementScreen.Hide();
    }
}
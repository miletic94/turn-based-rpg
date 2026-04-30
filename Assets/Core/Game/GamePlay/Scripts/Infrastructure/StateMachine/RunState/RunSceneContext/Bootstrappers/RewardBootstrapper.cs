using UnityEngine;

public class RewardBootstrapper : MonoBehaviour
{
    [SerializeField] RewardScreen RewardScreen;

    public void Show()
    {
        RewardScreen.Show();
    }
    public void Hide()
    {
        RewardScreen.Hide();
    }
}
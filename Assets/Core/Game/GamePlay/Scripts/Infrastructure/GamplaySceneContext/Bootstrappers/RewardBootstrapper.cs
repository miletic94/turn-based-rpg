using UnityEngine;

public class RewardBootstrapper : MonoBehaviour
{
    [SerializeField] RewardScreen RewardScreen;

    public void Load()
    {
        RewardScreen.Show();
    }
    public void Unload()
    {
        RewardScreen.Hide();
    }
}
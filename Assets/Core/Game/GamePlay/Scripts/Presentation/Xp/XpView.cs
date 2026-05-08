using UnityEngine;

public class XpView : MonoBehaviour
{
    [SerializeField] XpBarView _xpBarView;
    public int Value { get; private set; }

    public void UpdateXp(UpdateXpResult updateXpData)
    {
        _xpBarView.SetFill(updateXpData.CurrentXp / updateXpData.XpToNextLevel);
    }
}
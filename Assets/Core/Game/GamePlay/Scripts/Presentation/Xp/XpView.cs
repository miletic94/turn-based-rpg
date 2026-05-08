using UnityEngine;

public class XpView : MonoBehaviour
{
    [SerializeField] XpBarView _xpBarView;
    public int Value { get; private set; }

    public async Awaitable UpdateXp(UpdateXpResult updateXpData)
    {
        _xpBarView.SetFill((float)updateXpData.PreviousXp / updateXpData.PreviousXpToNextLevel);
        await Awaitable.WaitForSecondsAsync(1f);
        _xpBarView.SetFill((float)updateXpData.CurrentXp / updateXpData.CurrentXpToNextLevel);
        await Awaitable.WaitForSecondsAsync(1f);
    }
}
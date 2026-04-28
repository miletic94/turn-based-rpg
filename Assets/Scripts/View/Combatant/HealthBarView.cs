using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image fill;

    public void SetImmediate(float percent)
    {
        fill.fillAmount = percent;
    }
}
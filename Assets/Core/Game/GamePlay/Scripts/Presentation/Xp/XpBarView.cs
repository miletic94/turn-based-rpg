using UnityEngine;
using UnityEngine.UI;

public class XpBarView : MonoBehaviour
{
    [SerializeField] Image _fillImage;

    public void SetFill(float fillAmount)
    {
        _fillImage.fillAmount = Mathf.Clamp01(fillAmount);
    }
}
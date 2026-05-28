using System.Collections;
using TMPro;
using UnityEngine;

public class WarningTooltipView : MonoBehaviour, ITooltipView<WarningMessage>
{
    [SerializeField] TMP_Text _message;
    [SerializeField] private float _duration = 2f;
    private Coroutine _hideRoutine;
    public void Show(WarningMessage warning)
    {
        _message.SetText(warning.Message);
        gameObject.SetActive(true);

        if (_hideRoutine != null)
        {
            StopCoroutine(_hideRoutine);
        }
        _hideRoutine = StartCoroutine(HideAfterDelay());
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        _hideRoutine = null;
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(_duration);

        Hide();
    }
}
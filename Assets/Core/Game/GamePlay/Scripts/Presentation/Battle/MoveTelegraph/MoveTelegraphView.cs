using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MoveTelegraphView : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public async Awaitable ShowData(MoveTelegraphData data)
    {
        await ShowEffect(data);
    }

    private async Awaitable ShowEffect(MoveTelegraphData data)
    {
        text.text = data.Text;
        text.color = data.Color;
        gameObject.SetActive(true);
        await Awaitable.WaitForSecondsAsync(1f);
        gameObject.SetActive(false);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MoveTelegraphView : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public async Awaitable ShowData(List<MoveTelegraphData> dataList)
    {
        await ShowEffects(dataList);
    }
    private async Task ShowEffects(List<MoveTelegraphData> dataList)
    {
        foreach (var data in dataList)
        {
            await ShowEffect(data);
        }
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
using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardView : MonoBehaviour
{
    [SerializeField] RewardListView _list;

    public void ShowRewards(List<RewardItemData> rewards)
    {
        _list.Render(rewards);
    }
    public RewardItemView GetView(int id)
    {
        return _list.GetView(id);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardView : MonoBehaviour
{
    [SerializeField] private Transform _rewardContainer;
    [SerializeField] private RewardItemView _rewardItemPrefab;

    public void ShowRewards(List<Move> rewards, Action<Move> onRewardSelected)
    {
        foreach (var reward in rewards)
        {
            var rewardItem = Instantiate(_rewardItemPrefab, _rewardContainer);
            rewardItem.SetText(reward.Name);
            rewardItem.SetButtonAction(() => onRewardSelected(reward));
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardView : MonoBehaviour
{
    [SerializeField] private Transform _rewardContainer;
    [SerializeField] private RewardItemView _rewardItemPrefab;

    public void ShowRewards(List<Move> rewards, Action<Move> onRewardSelected, Action<Move> onRewardHovered)
    {
        // TODO: Implement pooling for reward items
        foreach (Transform child in _rewardContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (var reward in rewards)
        {
            var rewardItem = Instantiate(_rewardItemPrefab, _rewardContainer);
            rewardItem.SetText(reward.Name);
            rewardItem.SetOnRewardSelected(() => onRewardSelected(reward));
            rewardItem.SetOnRewardHovered(() => onRewardHovered(reward));
        }
    }
}
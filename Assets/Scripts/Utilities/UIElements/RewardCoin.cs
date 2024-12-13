using Manager;
using MonoSingleton;
using ObjectPools;
using TMPro;
using UnityEngine;

namespace Utilities.UIElements
{
    public class RewardCoin : MonoSingleton<RewardCoin>
    {
        private int _rewardCoin;
        [SerializeField] private TextMeshProUGUI rewardCoinText;

        public void SetPlayerRewardCoin()
        {
            _rewardCoin = (AgentPools.Instance.AgentCount * 12) + Random.Range(0, 15);
            rewardCoinText.text = _rewardCoin.ToString();
            CoinManager.Instance.EarnCoin(_rewardCoin);
        }
    }
}
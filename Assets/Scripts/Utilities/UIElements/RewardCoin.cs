using Manager;
using MonoSingleton;
using ObjectPools;
using TMPro;
using UnityEngine;

namespace Utilities.UIElements
{
    public class RewardCoin : MonoSingleton<RewardCoin>
    {
        private int _rewardCoin; // Kazanılan coin miktarı
        [SerializeField] private TextMeshProUGUI _rewardCoinText; // Kazanılan coin text'i 
        
        /// <para> Kazanılan coin miktarını ayarlar. </para>
        public void SetPlayerRewardCoin()
        {
            _rewardCoin = (AgentPools.Instance.AgentCount * 12) + Random.Range(0, 15);
            _rewardCoinText.text = _rewardCoin.ToString();
            CoinManager.Instance.EarnCoin(_rewardCoin);
        }
    }
}
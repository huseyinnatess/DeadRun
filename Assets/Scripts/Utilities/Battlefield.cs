using System;
using GoogleAdmob;
using GoogleAdmob.Interface;
using Manager;
using ObjectPools;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Utilities
{
    public class Battlefield : MonoBehaviour
    {
        private Scene _activeScene;
        private int _rewardCoin;
        private TextMeshProUGUI _rewardCoinText;
        private readonly CoinManager _coinManager = new();
        private IAdmob _intersititialAd = new InterstitialAD();
        private void Awake()
        {
            _activeScene = SceneManager.GetActiveScene();
        }

        private void Victory()
        {
            if (_activeScene.buildIndex == PlayerData.GetInt("EndLevel"))
                PlayerData.SetInt("EndLevel", PlayerData.GetInt("EndLevel") + 1);
            LevelPanelManager.VictoryPanel(true);
            _rewardCoin = (AgentPools.CharacterCount * 4) + Random.Range(0, 15);
            GameObject.FindWithTag("RewardCoin").GetComponent<TextMeshProUGUI>().text = _rewardCoin.ToString();
            _coinManager.EarnCoin(_rewardCoin);
        }
        private void Defeat()
        {
            LevelPanelManager.DefeatPanel(true);
        }
        
        public void WarResult(int enemyCount)
        {
            ShowIntersititalAd();
            if (enemyCount == 0)
                Victory();
            else
                Defeat();
        }

        private void ShowIntersititalAd()
        {
            _intersititialAd.ShowAd();
        }
    }
}
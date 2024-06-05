using Controller;
using GoogleAdmob;
using Manager;
using MonoSingleton;
using ObjectPools;
using UnityEngine.SceneManagement;
using Utilities.UIElements;

namespace Utilities
{
    public class Battlefield : MonoSingleton<Battlefield>
    {
        /// <summary>
        /// Savaş sonucuna göre Victory veya Defeat panelini aktif eder
        /// </summary>
        /// <param name="agentCount"></param>
        public void WarResult(int agentCount)
        {
            InterstitialAD.Instance.ShowAd();
            if (agentCount != 0)
                Victory();
            else
                Defeat();
        }

        /// <para> Oyuncuğu kazandığında son level, confetti effect'i, Victory Panel ve kazanılan
        /// coinlerin ayarlamasını yapar.</para>
        private void Victory()
        {
            PlayerLevel.SetPlayerEndLevel(SceneManager.GetActiveScene().buildIndex);
            ParticleEffectPool.Instance.ConfettiEffectPool(CharacterControl.Instance.transform);
            LevelPanelManager.Instance.VictoryPanel(true);
            RewardCoin.Instance.SetPlayerRewardCoin();
        }

        /// <para> Defeat Panel'ini açar</para>
        private void Defeat()
        {
            LevelPanelManager.Instance.DefeatPanel(true);
        }
        
    }
}
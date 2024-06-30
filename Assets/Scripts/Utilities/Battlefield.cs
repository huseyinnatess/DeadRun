using Controller;
using Controller.AgentsController;
using Manager;
using Manager.Audio.Utilities;
using MonoSingleton;
using ObjectPools;
using UnityEngine.SceneManagement;
using Utilities.UIElements;

namespace Utilities
{
    public class Battlefield : MonoSingleton<Battlefield>
    {
        private FxSounds _fxSounds; // Efektler scripti.

        #region Start
        private void Start()
        {
            _fxSounds = FxSounds.Instance;
        }
        #endregion

        /// <summary>
        /// Savaş sonucuna göre VictoryFx veya Defeat panelini aktif eder
        /// </summary>
        /// <param name="agentCount"></param>
        public void WarResult(int agentCount)
        {
          //  InterstitialAD.Instance.ShowAd();
            _fxSounds.CharacterRunFx.Stop();
            if (agentCount != 0)
                Victory();
            else
                Defeat();
        }

        /// <para> Oyuncuğu kazandığında son level, confetti effect'i, VictoryFx Panel ve kazanılan
        /// coinlerin ayarlamasını yapar.</para>
        private void Victory()
        {
            _fxSounds.VictoryFx.Play();
            PlayerLevel.SetPlayerEndLevel(SceneManager.GetActiveScene().buildIndex);
            ParticleEffectPool.Instance.ConfettiEffectPool(AgentController.GetActiveAgent());
            LevelPanelManager.Instance.VictoryPanel(true);
            RewardCoin.Instance.SetPlayerRewardCoin();
        }

        /// <para> Defeat Panel'ini açar ve ses ayarlarını günceller.</para>
        private void Defeat()
        {
            LevelPanelManager.Instance.DefeatPanel(true);
            _fxSounds.DefeatFx.Play();
            _fxSounds.CharacterRunFx.enabled = false;
            _fxSounds.FanFx.enabled = false;
        }
        
    }
}
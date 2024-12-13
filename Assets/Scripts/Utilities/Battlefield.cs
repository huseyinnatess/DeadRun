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
        private FxSounds _fxSounds;

        #region Start

        private void Start()
        {
            _fxSounds = FxSounds.Instance;
        }

        #endregion


        public void WarResult(int agentCount)
        {
            _fxSounds.CharacterRunFx.Stop();
            if (agentCount != 0)
                Victory();
            else
                Defeat();
        }


        private void Victory()
        {
            _fxSounds.VictoryFx.Play();
            PlayerLevel.SetPlayerEndLevel(SceneManager.GetActiveScene().buildIndex);
            ParticleEffectPool.Instance.ConfettiEffectPool(AgentController.GetActiveAgent());
            LevelPanelManager.Instance.VictoryPanel(true);
            RewardCoin.Instance.SetPlayerRewardCoin();
        }

        private void Defeat()
        {
            LevelPanelManager.Instance.DefeatPanel(true);
            _fxSounds.DefeatFx.Play();
            _fxSounds.CharacterRunFx.enabled = false;
            _fxSounds.FanFx.enabled = false;
        }
    }
}
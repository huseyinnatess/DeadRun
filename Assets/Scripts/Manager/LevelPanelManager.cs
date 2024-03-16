using MonoSingleton;
using UnityEngine;

namespace Manager
{
    public class LevelPanelManager : MonoSingleton<LevelPanelManager>
    {
        private static GameObject _victoryPanel;
        private static GameObject _defeatPanel;
        private static GameObject _pausePanel;

        #region Awake Start

        private void Awake()
        {
            _victoryPanel = GameObject.FindWithTag("VictoryPanel");
            _defeatPanel = GameObject.FindWithTag("DefeatPanel");
            _pausePanel = GameObject.FindWithTag("PausePanel");
        }

        private void Start()
        {
            _pausePanel.SetActive(false);
            _victoryPanel.SetActive(false);
            _defeatPanel.SetActive(false);
        }

        #endregion
        
        public void VictoryPanel(bool active)
        {
            _victoryPanel.SetActive(active);
        }

        public void DefeatPanel(bool active)
        {
            _defeatPanel.SetActive(active);
        }

        public void PausePanel(bool active)
        {
            _pausePanel.SetActive(active);
        }
    }
}
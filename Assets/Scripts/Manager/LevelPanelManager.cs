using MonoSingleton;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using AudioSettings = Utilities.AudioSettings;

namespace Manager
{
    public class LevelPanelManager : MonoSingleton<LevelPanelManager>
    {
        private GameObject _victoryPanel;
        private GameObject _defeatPanel;
        private GameObject _pausePanel;
        [HideInInspector]public GameObject GamePanels;
        [HideInInspector]public GameObject MainMenuPanels;

        private Slider[] _pausePanelSliders;
        #region Awake Start
        private void Awake()
        {
            GetReferences();
        }
        private void GetReferences()
        {
            _victoryPanel = GameObject.FindWithTag("VictoryPanel");
            _defeatPanel = GameObject.FindWithTag("DefeatPanel");
            _pausePanel = GameObject.FindWithTag("PausePanel");
            GamePanels = GameObject.FindWithTag("GamePanels");
            MainMenuPanels = GameObject.FindWithTag("MainMenuPanels");
            _pausePanelSliders = _pausePanel.GetComponentsInChildren<Slider>();
        }
        
        private void Start()
        {
            SetPausePanelSliders();
            _pausePanel.SetActive(false);
            _victoryPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            GamePanels.SetActive(false);
        }
        
        private void SetPausePanelSliders()
        {
            _pausePanelSliders[0].value = AudioSettings.GetSoundSliderValue();
            _pausePanelSliders[1].value = AudioSettings.GetMusicSliderValue();
        }
        #endregion
        
        public void VictoryPanel(bool state)
        {
            _victoryPanel.SetActive(state);
        }

        public void DefeatPanel(bool state)
        {
            _defeatPanel.SetActive(state);
        }

        public void PausePanel(bool state)
        {
            _pausePanel.SetActive(state);
        }

        public void MainMenuPanel(bool state)
        {
            MainMenuPanels.SetActive(state);
        }
    }
}
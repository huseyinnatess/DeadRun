using Manager.Audio;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class LevelPanelManager : MonoSingleton<LevelPanelManager>
    {
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject defeatPanel;
        [SerializeField] private GameObject pausePanel;
        public GameObject CreditsPanel;
        [HideInInspector] public GameObject GamePanels;
        [HideInInspector] public GameObject MainMenuPanels;
        private Slider[] _pausePanelSliders;

        #region Awake Start

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            GamePanels = GameObject.FindWithTag("GamePanels");
            MainMenuPanels = GameObject.FindWithTag("MainMenuPanels");
            _pausePanelSliders = pausePanel.GetComponentsInChildren<Slider>();
        }

        private void Start()
        {
            UpdatePausePanelSliders();
            GamePanels.SetActive(false);
        }

        #endregion


        public void UpdatePausePanelSliders()
        {
            _pausePanelSliders[0].value = AudioManager.GetSoundSliderValue();
            _pausePanelSliders[1].value = AudioManager.GetFxSliderValue();
        }


        public void VictoryPanel(bool state)
        {
            victoryPanel.SetActive(state);
        }


        public void DefeatPanel(bool state)
        {
            defeatPanel.SetActive(state);
        }


        public void PausePanel(bool state)
        {
            Time.timeScale = System.Convert.ToInt32(!state);
            pausePanel.SetActive(state);
        }


        public void MainMenuPanel(bool state)
        {
            MainMenuPanels.SetActive(state);
        }
    }
}
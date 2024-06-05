using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using AudioSettings = Utilities.AudioSettings;

namespace Manager
{
    public class LevelPanelManager : MonoSingleton<LevelPanelManager>
    {
        private GameObject _victoryPanel; // Zafer paneli
        private GameObject _defeatPanel; // Yenilgi paneli
        private GameObject _pausePanel; // Duraklatma paneli
        [HideInInspector]public GameObject GamePanels; // Oyun içindeki Victory, Defeat, Pause panellerinin parent'ı
        [HideInInspector]public GameObject MainMenuPanels; // AnaMenü panellerinin parent'ı

        private Slider[] _pausePanelSliders; // Duraklatma panelinin slider'ları
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
        
        /// <summary>
        /// State parametresine göre Victory panelin aktifliğini ayarlar.
        /// </summary>
        /// <param name="state"> Panel'in aktiflik durumu</param>
        public void VictoryPanel(bool state)
        {
            _victoryPanel.SetActive(state);
        }
        
        /// <summary>
        /// State parametresine göre Defeat panelin aktifliğini ayarlar.
        /// </summary>
        /// <param name="state"> Panel'in aktiflik durumu </param>
        public void DefeatPanel(bool state)
        {
            _defeatPanel.SetActive(state);
        }
        
        /// <summary>
        ///  State parametresine göre Pause panelin aktifliğini ayarlar.
        /// </summary>
        /// <param name="state"> Panel'in aktiflik durumu </param>
        public void PausePanel(bool state)
        {
            _pausePanel.SetActive(state);
        }
        
        /// <summary>
        ///  State parametresine göre AnaMenü panellerinin aktifliğini ayarlar.
        /// </summary>
        /// <param name="state"> Panel'in aktiflik durumu </param>
        public void MainMenuPanel(bool state)
        {
            MainMenuPanels.SetActive(state);
        }
    }
}
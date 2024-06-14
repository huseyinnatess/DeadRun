using Manager.Audio;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Manager
{
    public class LevelPanelManager : MonoSingleton<LevelPanelManager>
    {
        [SerializeField] private GameObject _victoryPanel; // Zafer paneli
        [SerializeField] private GameObject _defeatPanel; // Yenilgi paneli
        [SerializeField] private GameObject _pausePanel; // Duraklatma paneli
        
        [HideInInspector] public GameObject GamePanels; // Oyun içindeki VictoryFx, Defeat, Pause panellerinin parent'ı
        [HideInInspector] public GameObject MainMenuPanels; // AnaMenü panellerinin parent'ı

        private Slider[] _pausePanelSliders; // Duraklatma panelinin slider'ları

        #region Awake Start

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            GamePanels = GameObject.FindWithTag("GamePanels");
            MainMenuPanels = GameObject.FindWithTag("MainMenuPanels");
            _pausePanelSliders = _pausePanel.GetComponentsInChildren<Slider>();
        }

        private void Start()
        {
            UpdatePausePanelSliders();
            GamePanels.SetActive(false);
        }
        #endregion
        
        /// <summary>
        /// Duraklatma paneli'nin Slider'larını günceller.
        /// </summary>
        public void UpdatePausePanelSliders()
        {
            _pausePanelSliders[0].value = AudioManager.GetSoundSliderValue();
            _pausePanelSliders[1].value = AudioManager.GetFxSliderValue();
        }
        
        /// <summary>
        /// State parametresine göre VictoryFx panelin aktifliğini ayarlar.
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
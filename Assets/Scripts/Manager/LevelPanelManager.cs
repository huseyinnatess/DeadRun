using Manager.Audio;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class LevelPanelManager : MonoSingleton<LevelPanelManager>
    {
        [SerializeField] private GameObject victoryPanel; // Zafer paneli
        [SerializeField] private GameObject defeatPanel; // Yenilgi paneli
        [SerializeField] private GameObject pausePanel; // Duraklatma paneli
        
        public GameObject CreditsPanel; // Krediler paneli
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
            _pausePanelSliders = pausePanel.GetComponentsInChildren<Slider>();
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
            victoryPanel.SetActive(state);
        }

        /// <summary>
        /// State parametresine göre Defeat panelin aktifliğini ayarlar.
        /// </summary>
        /// <param name="state"> Panel'in aktiflik durumu </param>
        public void DefeatPanel(bool state)
        {
            defeatPanel.SetActive(state);
        }

        /// <summary>
        ///  State parametresine göre Pause panelin'in ve parentlarının aktifliğini ayarlar.
        /// </summary>
        /// <param name="state"> Panel'in aktiflik durumu </param>
        public void PausePanel(bool state)
        {
            Time.timeScale = System.Convert.ToInt32(!state);
            pausePanel.SetActive(state);
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